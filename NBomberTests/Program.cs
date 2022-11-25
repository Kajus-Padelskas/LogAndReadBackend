// See https://aka.ms/new-console-template for more information

using NBomber.Contracts;
using NBomber.CSharp;

using var httpClient = new HttpClient();

var step = Step.Create("Get Users Endpoint", async context =>
{
    var response = await httpClient.GetAsync("https://localhost:44365/user");
    return response.IsSuccessStatusCode
        ? Response.Ok(response.StatusCode)
        : Response.Fail() ;
});

var scenario = ScenarioBuilder.CreateScenario("Get Users", step)
    .WithWarmUpDuration(TimeSpan.FromSeconds(5))
    .WithLoadSimulations(
        Simulation.InjectPerSec(rate: 100, during: TimeSpan.FromSeconds(60))
    ); ;

NBomberRunner
    .RegisterScenarios(scenario)
    .Run();