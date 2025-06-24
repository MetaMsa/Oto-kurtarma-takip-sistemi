using Microsoft.EntityFrameworkCore;

public class AdminCheckMiddleware
{
    private readonly RequestDelegate _reqDel;
    private readonly AppDbContext _context;

    public AdminCheckMiddleware(RequestDelegate reqdel)
    {
        _reqDel = reqdel;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.ToString();

        if (path.StartsWith("/Admin", StringComparison.OrdinalIgnoreCase))
        {
            using var scope = context.RequestServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var user = await db.Users.Include(u => u.RolesModel).FirstOrDefaultAsync(u => u.username == context.User.Identity.Name);

            if (user.RolesModel.Role != "Admin")
            {
                context.Response.Redirect("/Home");
                return;
            }
        }

        await _reqDel(context);
    }
}