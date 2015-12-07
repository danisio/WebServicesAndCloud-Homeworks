namespace MusicCatalog.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Data.Contracts;
    using Data.UnitOfWork;
    using Models;
    using MusicCatalog.Models;

    public class AlbumsController : ApiController
    {
        private readonly ICatalogData data;

        public AlbumsController()
            : this(new CatalogData())
        {
        }

        public AlbumsController(ICatalogData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IQueryable<AlbumDataModel> All()
        {
            return this.data.Albums
                .All()
                .Select(AlbumDataModel.FromAlbum);
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var album = this.data.Albums
                .Search(a => a.Id == id)
                .Select(AlbumDataModel.FromAlbum);

            if (!album.Any())
            {
                return this.NotFound();
            }

            return this.Ok(album);
        }

        [HttpPut]
        public IHttpActionResult Update(AlbumDataModel album)
        {
            if (!this.ModelState.IsValid || album == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var existingAlbum = this.data.Albums.FindById(album.Id);

            if (existingAlbum == null)
            {
                return this.BadRequest();
            }

            existingAlbum.Title = album.Title;
            existingAlbum.Producer = album.Producer;

            this.data.SaveChanges();

            album.Id = existingAlbum.Id;
            return this.Ok(album);
        }

        [HttpPost]
        public IHttpActionResult Create(AlbumDataModel album)
        {
            if (!this.ModelState.IsValid || album == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var newAlbum = new Album()
            {
                Title = album.Title,
                Producer = album.Producer
            };

            this.data.Albums.Add(newAlbum);
            this.data.Albums.SaveChanges();

            album.Id = newAlbum.Id;
            return this.Ok(album);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var album = this.data.Albums.FindById(id);
            if (album == null)
            {
                return this.NotFound();
            }

            this.data.Albums.Delete(album);
            this.data.SaveChanges();

            return this.Ok(album);
        }

        [HttpGet]
        public IHttpActionResult Songs(int id)
        {
            var existingAlbum = this.data.Albums.FindById(id);

            if (existingAlbum == null)
            {
                return this.NotFound();
            }

            var songs = existingAlbum.Songs
                .AsQueryable()
                .Select(SongDataModel.FromSong);

            return this.Ok(songs);
        }
    }
}
