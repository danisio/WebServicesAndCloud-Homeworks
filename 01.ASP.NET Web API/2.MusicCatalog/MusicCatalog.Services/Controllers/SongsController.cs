namespace MusicCatalog.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Data.Contracts;
    using Data.UnitOfWork;
    using Models;
    using MusicCatalog.Models;

    public class SongsController : ApiController
    {
        private readonly ICatalogData data;

        public SongsController()
            : this(new CatalogData())
        {
        }

        public SongsController(ICatalogData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IQueryable<SongDataModel> All()
        {
            return this.data.Songs
                .All()
                .Select(SongDataModel.FromSong);
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var song = this.data.Songs
                .Search(s => s.Id == id)
                .Select(SongDataModel.FromSong);

            if (!song.Any())
            {
                return this.NotFound();
            }

            return this.Ok(song);
        }

        [HttpPut]
        public IHttpActionResult Update(SongDataModel song)
        {
            if (!this.ModelState.IsValid || song == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var existingSong = this.data.Songs.FindById(song.Id);

            if (existingSong == null)
            {
                return this.BadRequest();
            }

            existingSong.Title = song.Title;
            existingSong.Year = song.Year;
            existingSong.Genre = song.Genre;
            existingSong.AlbumId = song.AlbumId;

            this.data.SaveChanges();

            song.Id = existingSong.Id;
            return this.Ok(song);
        }

        [HttpPost]
        public IHttpActionResult Create(SongDataModel song)
        {
            if (!this.ModelState.IsValid || song == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var newSong = new Song()
            {
                Title = song.Title,
                Year = song.Year,
                Genre = song.Genre,
                AlbumId = song.AlbumId
            };

            this.data.Songs.Add(newSong);
            this.data.Songs.SaveChanges();

            song.Id = newSong.Id;
            return this.Ok(song);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var song = this.data.Songs.FindById(id);
            if (song == null)
            {
                return this.NotFound();
            }

            this.data.Songs.Delete(song);
            this.data.SaveChanges();

            return this.Ok(song);
        }
    }
}
