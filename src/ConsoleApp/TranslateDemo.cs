namespace MultilingualExploration;

public class TranslateDemo
{

	private readonly CultureInfo systemCultureInto = CultureInfo.CurrentCulture;

	public static async Task<long> ExecuteDemoAsync(ITranslator translator, string serviceColorCode)
		=> await new TranslateDemo().InternalExecuteDemoAsync(translator, serviceColorCode);

	private async Task<long> InternalExecuteDemoAsync(ITranslator translator, string serviceColorCode)
	{

		Layout layout = new Layout("Root")
			.SplitRows(
				new Layout("System").Size(6)
					.SplitColumns(
						new Layout("SystemCulture"),
						new Layout("TranslationMethod")
						),
				new Layout("Translated")
				.SplitColumns(
					new Layout("Left")
						.SplitRows(
							new Layout("en-US"),
							new Layout("es-MX"),
							new Layout("fr-FR"),
							new Layout("cs-CZ")),
					new Layout("Right")
						.SplitRows(
							new Layout("en-GB"),
							new Layout("es-PR"),
							new Layout("de-DE"),
							new Layout("fil-PH"))));

		layout["System"]["SystemCulture"].Update(new Panel(await translator.GetGreetingAsync(systemCultureInto)).Expand());
		layout["System"]["TranslationMethod"].Update(new Panel(Align.Center(new Markup($"[{serviceColorCode}]{translator.TranslatorName}[/]"), VerticalAlignment.Middle)).Expand());
		layout["Translated"]["Left"]["en-US"].Update(new Panel(Align.Center(new Markup("[slowblink]Translating[/]"), VerticalAlignment.Middle)).Expand());
		layout["Translated"]["Left"]["es-MX"].Update(new Panel(Align.Center(new Markup("[slowblink]Translating[/]"), VerticalAlignment.Middle)).Expand());
		layout["Translated"]["Left"]["fr-FR"].Update(new Panel(Align.Center(new Markup("[slowblink]Translating[/]"), VerticalAlignment.Middle)).Expand());
		layout["Translated"]["Left"]["cs-CZ"].Update(new Panel(Align.Center(new Markup("[slowblink]Translating[/]"), VerticalAlignment.Middle)).Expand());
		layout["Translated"]["Right"]["en-GB"].Update(new Panel(Align.Center(new Markup("[slowblink]Translating[/]"), VerticalAlignment.Middle)).Expand());
		layout["Translated"]["Right"]["es-PR"].Update(new Panel(Align.Center(new Markup("[slowblink]Translating[/]"), VerticalAlignment.Middle)).Expand());
		layout["Translated"]["Right"]["de-DE"].Update(new Panel(Align.Center(new Markup("[slowblink]Translating[/]"), VerticalAlignment.Middle)).Expand());
		layout["Translated"]["Right"]["fil-PH"].Update(new Panel(Align.Center(new Markup("[slowblink]Translating[/]"), VerticalAlignment.Middle)).Expand());
		AnsiConsole.Write(layout);

		Stopwatch stopwatch = Stopwatch.StartNew();

		layout["Translated"]["Left"]["en-US"].Update(new Panel(await translator.GetGreetingAsync(systemCultureInto)).Expand());
		layout["Translated"]["Left"]["es-MX"].Update(new Panel(await translator.GetGreetingAsync(new("es-MX"))).Expand());
		layout["Translated"]["Left"]["fr-FR"].Update(new Panel(await translator.GetGreetingAsync(new("fr-FR"))).Expand());
		layout["Translated"]["Left"]["cs-CZ"].Update(new Panel(await translator.GetGreetingAsync(new("cs-CZ"))).Expand());
		layout["Translated"]["Right"]["en-GB"].Update(new Panel(await translator.GetGreetingAsync(new("en-GB"))).Expand());
		layout["Translated"]["Right"]["es-PR"].Update(new Panel(await translator.GetGreetingAsync(new("es-PR"))).Expand());
		layout["Translated"]["Right"]["de-DE"].Update(new Panel(await translator.GetGreetingAsync(new("de-DE"))).Expand());
		layout["Translated"]["Right"]["fil-PH"].Update(new Panel(await translator.GetGreetingAsync(new("fil-PH"))).Expand());

		stopwatch.Stop();
		layout["System"]["TranslationMethod"].Update(new Panel(Align.Center(new Markup($"[{serviceColorCode}]{translator.TranslatorName}[/]\nMilliseconds: {stopwatch.ElapsedMilliseconds:N0}\nCharacters Translated: {translator.CharactersConverted:N0}"), VerticalAlignment.Middle)).Expand());

		AnsiConsole.Write(layout);
		Console.ReadLine();

		return stopwatch.ElapsedMilliseconds;

	}

}