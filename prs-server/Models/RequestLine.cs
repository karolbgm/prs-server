using System.Text.Json.Serialization;

namespace prs_server.Models;

public class RequestLine
{
    public int Id { get; set; }
    public int Quantity { get; set; } = 1;

    //FK
    public int RequestId { get; set; }
    [JsonIgnore] //if you don't add this, this will pull the request and the request will pull lines which will pull request...
    public virtual Request? Request { get; set; }

    public int ProductId { get; set; }
    public virtual Product? Product { get; set; }
}
