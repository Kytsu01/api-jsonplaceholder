namespace JsonPlaceholderImporter.Dtos
{
    public class UserCreateDto
    {
        public string Name { get; set; } = "";
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Website { get; set; } = "";
        public AddressCreateDto? Address { get; set; }
        public CompanyCreateDto? Company { get; set; }
    }

    public class AddressCreateDto
    {
        public string Street { get; set; } = "";
        public string City { get; set; } = "";
        public string ZipCode { get; set; } = "";
        public GeolocationCreateDto? Geo { get; set; }
    }

    public class GeolocationCreateDto
    {
        public string Lat { get; set; } = "";
        public string Lng { get; set; } = "";
    }

    public class CompanyCreateDto 
    {
        public string Name { get; set; } = "";
        public string CatchPhrase { get; set; } = "";
        public string Bs { get; set; } = "";
    }

}
