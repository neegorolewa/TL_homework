using Domain.Entities;

namespace Domain.Contracts;

public class PropertyWithRoomTypeDto
{
    public PropertyDto Property { get; set; }
    public List<RoomTypeDto> RoomTypes { get; set; } = [];
}
