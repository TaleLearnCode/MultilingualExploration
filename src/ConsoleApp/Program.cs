DemoKeys demoKeys = new();

long resxElapsedTime = await TranslateDemo.ExecuteDemoAsync(new ResxTranslator(), "#512bd4");
long azureElapsedTime = await TranslateDemo.ExecuteDemoAsync(new AzureTranslator(demoKeys.AzureAITranslator), "#007FFF");
long awsElapsedTime = await TranslateDemo.ExecuteDemoAsync(new AWSTranslator(), "#ff9900");

Console.Clear();
AnsiConsole.Write(new BarChart()
		.Width(60)
		.Label("[bold underline]Translation Execution Times[/]")
		.CenterLabel()
		.AddItem("Resource Files", resxElapsedTime, Color.MediumPurple)
		.AddItem("Azure AI Translator", azureElapsedTime, Color.SkyBlue1)
		.AddItem("AWS Translate", awsElapsedTime, Color.Orange1));
Console.WriteLine();
Console.WriteLine("Press any key to exit...");
Console.ReadKey(true);