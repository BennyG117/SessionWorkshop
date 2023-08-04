using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;

namespace SessionWorkshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
// Home Page view
    public IActionResult Index()
    {
        return View();
    }


// Login - POST
    [HttpPost("Login")]
    public IActionResult Login(string UserName, int CurrentNum)
    {
        HttpContext.Session.SetString("UserName", UserName);
        HttpContext.Session.SetInt32("CurrentNum", 17);
        return RedirectToAction("Dashboard");
    }

    // Dashboard - view once logged in
    [HttpGet("Dashboard")]
    public IActionResult Dashboard()
    {
        if (HttpContext.Session.GetString("UserName") == null)
        {
            return RedirectToAction("Index");
        }
        return View();
    }

    //!Logout - logout, clear session & return home
    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        // If you want to remove only one key from our session, use .Remove()!
        // HttpContext.Session.Remove("UserName");
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

//! +1 post/route
    [HttpPost("plusone")]
    public IActionResult PlusOne()
    {
        if (HttpContext.Session.GetString("UserName") == null)
        {
            return RedirectToAction("Dashboard");
        }
        int IntVariable = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentNum"));
        IntVariable +=1;
        HttpContext.Session.SetInt32("CurrentNum", IntVariable);

        return RedirectToAction("Dashboard");

    }

//! -1 post/route
    [HttpPost("minusone")]
    public IActionResult MinusOne()
    {
        if (HttpContext.Session.GetString("UserName") == null)
        {
            return RedirectToAction("Dashboard");
        }
        int IntVariable = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentNum"));
        IntVariable -=1;
        HttpContext.Session.SetInt32("CurrentNum", IntVariable);

        return RedirectToAction("Dashboard");

    }


//! x2 post/route
    [HttpPost("timestwo")]
    public IActionResult TimesTwo()
    {
        if (HttpContext.Session.GetString("UserName") == null)
        {
            return RedirectToAction("Dashboard");
        }
        int IntVariable = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentNum"));
        IntVariable *=2;
        HttpContext.Session.SetInt32("CurrentNum", IntVariable);

        return RedirectToAction("Dashboard");

    }


//! Random +(1-10) post/route
    [HttpPost("randomadd")]
    public IActionResult RandomAdd()
    {
        if (HttpContext.Session.GetString("UserName") == null)
        {
            return RedirectToAction("Dashboard");
        }
        int IntVariable = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentNum"));
        Random random = new Random();
        int randomValue = random.Next(1,11);
        IntVariable += randomValue;
        HttpContext.Session.SetInt32("CurrentNum", IntVariable);

        return RedirectToAction("Dashboard");

    }



// Default Privacy Page
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
