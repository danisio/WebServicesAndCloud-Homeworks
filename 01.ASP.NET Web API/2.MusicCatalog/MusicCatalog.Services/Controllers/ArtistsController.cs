namespace MusicCatalog.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Data.Contracts;
    using Data.UnitOfWork;
    using Models;
    using MusicCatalog.Models;

    public class ArtistsController : ApiController
    {
        private readonly ICatalogData data;

        public ArtistsController()
            : this(new CatalogData())
        {
        }

        public ArtistsController(ICatalogData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IQueryable<ArtistDataModel> All()
        {
            return this.data.Artists
                .All()
                .Select(ArtistDataModel.FromStudent);
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var artist = this.data.Artists
                .Search(a => a.Id == id)
                .Select(ArtistDataModel.FromStudent);

            if (!artist.Any())
            {
                return this.NotFound();
            }

            return this.Ok(artist);
        }

        [HttpPut]
        public IHttpActionResult Update(ArtistDataModel artist)
        {
            if (!this.ModelState.IsValid || artist == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var existingArtist = this.data.Artists.FindById(artist.Id);

            if (existingArtist == null)
            {
                return this.BadRequest();
            }

            existingArtist.Name = artist.Name;
            existingArtist.Country = artist.Country;
            existingArtist.DateOfBirth = artist.DateOfBirth;

            this.data.SaveChanges();

            artist.Id = existingArtist.Id;
            return this.Ok(artist);
        }

        [HttpPost]
        public IHttpActionResult Create(ArtistDataModel artist)
        {
            if (!this.ModelState.IsValid || artist == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var newArtist = new Artist()
            {
                Name = artist.Name,
                Country = artist.Country,
                DateOfBirth = artist.DateOfBirth,
            };

            this.data.Artists.Add(newArtist);
            this.data.Artists.SaveChanges();

            artist.Id = newArtist.Id;
            return this.Ok(artist);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var artist = this.data.Artists.FindById(id);
            if (artist == null)
            {
                return this.NotFound();
            }

            this.data.Artists.Delete(artist);
            this.data.SaveChanges();

            return this.Ok(artist);
        }
    }
}
