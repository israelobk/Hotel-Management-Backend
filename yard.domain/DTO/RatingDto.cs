using System.ComponentModel.DataAnnotations;

namespace yard.domain.dto
{
    public class RatingDto
    {
        [Range(0, 10)] public int Ratings { get; set; }
        [DataType(DataType.Text)] public string Comment { get; set; }
    }
}