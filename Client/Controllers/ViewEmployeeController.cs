﻿using API.DTOs.Employees;
using Client.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers;

public class ViewEmployeeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}