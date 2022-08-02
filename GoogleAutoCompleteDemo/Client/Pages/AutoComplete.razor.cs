using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Geocoding;
using GoogleApi.Entities.Places.AutoComplete.Request;
using GoogleApi.Entities.Places.AutoComplete.Response;
using GoogleApi.Entities.Places.Common;
using GoogleAutoCompleteDemo.Client;
using Microsoft.AspNetCore.Components;
using Org.BouncyCastle.Asn1.X509;
using System.Globalization;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GoogleAutoCompleteDemo.Pages;

public partial class AutoComplete
	{
	[Inject]
	public HttpClient? Http { get; set; }

	public static string? GoogleApiKey;
	public List<Prediction> AddressOptions { get; protected set; } = new();
	public string SelectedOptionId { get; set; } = "";
	public Prediction? SelectedOption => AddressOptions != null ? AddressOptions.FirstOrDefault(o => o.PlaceId == SelectedOptionId) : null;


	public async Task<List<Prediction>> GetOptions(string Address)
		{
		AddressOptions.Clear();
		HttpClient client = new();
		try
			{
			var list = await Http.GetFromJsonAsync<PlacesAutoCompleteResponse>($"Google/PlacesAutocomplete?Language=en&Target={Address}");
			if(list == null || list.Predictions == null)
				return AddressOptions;
			if(list.Status != GoogleApi.Entities.Common.Enums.Status.Ok)
				return AddressOptions; //TODO: error indication
			foreach(var a in list.Predictions)
				AddressOptions.Add(a);
			return AddressOptions;
			}
		catch(Exception Ex)
			{
			return AddressOptions;
			}
		}

	public void PredictionSelected(string Selected)
		{
		SelectedOptionId=Selected;
		}

	private async Task OnDestinationChanged()
		{
		try
			{
			}
		catch(Exception Ex)
			{

			}

		}
	}