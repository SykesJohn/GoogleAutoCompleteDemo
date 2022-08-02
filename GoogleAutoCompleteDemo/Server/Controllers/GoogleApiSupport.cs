using GoogleApi.Entities.Places.AutoComplete.Request;
using GoogleApi.Entities.Places.AutoComplete.Response;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace GoogleAutoCompleteDemo.Server.Controllers;

[ApiController]
//[Route("[controller]")]
public class GoogleApiSupport:ControllerBase
	{
	private readonly IConfiguration _config;
	private string _GoogleApiKey;

	public GoogleApiSupport(IConfiguration Configuration)
		{
		_config=Configuration;
		_GoogleApiKey=_config.GetValue<string>("GoogleApiKey");
		}

	/// <summary>
	/// Calls the Google Place Autocomplete predictor.
	/// </summary>
	/// <param name="Target">The target text to match.</param>
	/// <param name="Language">The language to return the results in.</param>
	/// <returns>The results of the search including a list of candidates.</returns>
	[HttpGet("Google/PlacesAutocomplete")]
	public async Task<ActionResult<PlacesAutoCompleteResponse>> PlacesAutocomplete(string Target, string Language)
		{
		PlacesAutoCompleteRequest req = new()
			{
			Input=Target,
			Language=Language=="fr" ? GoogleApi.Entities.Common.Enums.Language.FrenchCanada : GoogleApi.Entities.Common.Enums.Language.English,
			Key=_GoogleApiKey,
			Components=new List<KeyValuePair<GoogleApi.Entities.Common.Enums.Component, string>>()
				{
				new KeyValuePair<GoogleApi.Entities.Common.Enums.Component, string>(GoogleApi.Entities.Common.Enums.Component.Country, "ca"),
				new KeyValuePair<GoogleApi.Entities.Common.Enums.Component, string>(GoogleApi.Entities.Common.Enums.Component.Country, "us")
				}
			};
		var candidates = await GoogleApi.GooglePlaces.AutoComplete.QueryAsync(req);
		return Ok(candidates);
		}
	}
