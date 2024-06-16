using AutoMapper;
using HotelProject.BusinessLayer.Abstract;
using HotelProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelProject.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoomController : ControllerBase
	{
		private readonly IRoomService _roomService;
		private readonly IMapper _mapper;

		public RoomController(IRoomService roomService, IMapper mapper)
		{
			_roomService = roomService;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var values=_roomService.TGetList();
			return Ok(values);
		}

		[HttpPost]
		public IActionResult AddRoom(Room room)
		{
			_roomService.TInsert(room);
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteRoom(int id)
		{
			var value=_roomService.TGetByID(id);
			_roomService.TDelete(value);
			return Ok();
		}

		[HttpPut]
		public IActionResult UpdateRoom(int id)
		{
			var value = _roomService.TGetByID(id);
			_roomService.TUpdate(value);
			return Ok();
		}

		[HttpGet("{id}")]
		public IActionResult GetRoom(int id)
		{
			var values = _roomService.TGetByID(id);
			return Ok(values);
		}
	}
}
