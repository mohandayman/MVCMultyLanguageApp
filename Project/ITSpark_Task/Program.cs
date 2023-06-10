using ITSpark_Task;
using ITSpark_Task.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.CodeAnalysis.Host;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


#region Localizaion Services Configration 

// add Language Service 

builder.Services.AddSingleton<LanguageService>();

// Spacify The Language Culture Localization Folder

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


// builder.Services.AddMvc() => This line adds the MVC services to the application's dependency injection container. It enables MVC-related functionality in the application
//.AddViewLocalization() => This method configures the localization services for views. It enables the localization of view files (.cshtml) based on the requested culture
//.AddDataAnnotationsLocalization(options => { ... }): This method configures data annotations localization. Data annotations are attributes applied to model properties to define validation rules, error messages, and other metadata. By adding data annotations localization, you enable the localization of error messages and other data annotation-related texts
//
//options.DataAnnotationLocalizerProvider = (type, factory) => { ... }: This line sets the DataAnnotationLocalizerProvider
//property of the options object. The DataAnnotationLocalizerProvider is responsible for providing a IStringLocalizer for data annotations.
//It is a delegate that takes two parameters: the type of the resource class and a factory that can be used to create a IStringLocalizer.

//var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);:
//This line retrieves the assembly name of the SharedResource class. It uses reflection to get the assembly name of the class and assigns it to the assemblyName variable.

//return factory.Create("ShareResource", assemblyName.Name);: This line creates a IStringLocalizer by calling the Create method of the factory object. It specifies the resource base name as "ShareResource" and the assembly name as assemblyName.Name. This will be the resource used for localizing data annotation messages.


builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options => {
    options.DataAnnotationLocalizerProvider = (type, factory) => {
        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
        return factory.Create("SharedResource", assemblyName.Name);
    };
});

//builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-EG")
        // Add more cultures as needed
    };

    options.DefaultRequestCulture = new RequestCulture(culture: "ar-EG", uiCulture: "ar-EG");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());

});

#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<SchoolSystemContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

#region  Configure Localization PipeLine 

//// add suppported Languges 
//var SupportedCultures = new string[]
//{
//    "en" , "ar"
//};

//// Set the Localiztion options 
//var LocalizationOption = new RequestLocalizationOptions().SetDefaultCulture(SupportedCultures[1])
//                                                         .AddSupportedCultures(SupportedCultures)
//                                                         .AddSupportedUICultures(SupportedCultures);


#endregion

//app.UseRequestLocalization(LocalizationOption); // add MiddleWare for Loclization and use the localiztion options that i user before 
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Students}/{action=Index}/{id?}");

app.Run();
