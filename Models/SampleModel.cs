namespace Image_Generating_APIs.Models
{
    public class SampleModel
    {
    }

    public class Input
    {
        public string? prompt { get; set; }
        public short? n { get; set; }
        public string? size { get; set; }
    }

    public class Link
    {
        public string? Url { get; set; }
    }

    // model for the DALL E api response
    public class ResponseModel
    {
        public long Created { get; set; }
        public List<Link>? Data { get; set; }
    }
}
