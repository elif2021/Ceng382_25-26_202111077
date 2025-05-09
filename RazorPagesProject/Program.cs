var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // 30 dakika boşta kalınca oturumu kapatır
    options.Cookie.HttpOnly = true;                 // güvenlik için
    options.Cookie.IsEssential = true;             // GDPR uyumu için
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();  // Session middleware'ini ekliyoruz (önemli)

app.UseAuthorization();

app.MapRazorPages();

app.Run();
