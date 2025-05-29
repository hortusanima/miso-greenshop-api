using miso_greenshop_api.Dtos.Plants;

namespace miso_greenshop_api.Application.Models
{
    public class GetPlantsResponse
    {
        public List<GetPlantDto>? Plants { get; set; }
        public int TotalNumber { get; set; }
    }
}
