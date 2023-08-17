using API.DTOs.Rooms;
using API.Models;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;
[Authorize(Roles ="Manager,Admin")]

public class RoomController : Controller
{
    private readonly IRoomRepository repository;

    public RoomController(IRoomRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var result = await repository.Get();
        var ListRoom = new List<Room>();

        if (result.Data != null)
        {
            ListRoom = result.Data.ToList();
        }
        return View(ListRoom);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Room room)
    {
        var result = await repository.Post(room);

        if (result.Code == 200)
        {
            RedirectToAction("Index");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var result = await repository.Get(id);
        var ListRoom = new Room();

        if (result.Data != null)
        {
            ListRoom = result.Data;
        }
        return View(ListRoom);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Room room)
    {
        var result = await repository.Put(room.Guid, room);

        if (result.Code == 200)
        {
            TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
            return RedirectToAction("Index", "Room");
        }
        return RedirectToAction(nameof(Edit));
    }

}