using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class langVar
{
	public string label;
	public int size;
}

public class LanguageVars
{
	public class Main_menu
	{
		public langVar StartGame = new langVar();
		public langVar Config = new langVar();
		public langVar About = new langVar();
		public langVar BestScore = new langVar();
	}

	public class Config_menu
	{
		public langVar Configuration = new langVar();
		public langVar Quality = new langVar();
		public langVar Performance = new langVar();
		public langVar BestQuality = new langVar();
		public langVar Language = new langVar();
	}

	public class Game_over
	{
		public langVar PlayAgain = new langVar();
		public langVar MainMenu = new langVar();
	}

	public class Others
	{
		public string Loading;
		public string Copyright;
		public string TouchStart;
	}

	public SystemLanguage language;
	public Main_menu main_menu = new Main_menu();
	public Config_menu config_menu = new Config_menu();
	public Game_over game_over = new Game_over();
	public Others others = new Others();
}

public static class Language {
	public static LanguageVars languageVars = new LanguageVars();

	public static void readLanguage(string language)
	{
		if (language != "pt_BR" && language != "en_US") language = "en_US";

		switch(language)
		{
			case "pt_BR": languageVars.language = SystemLanguage.Portuguese; break;
			case "en_US": languageVars.language = SystemLanguage.English; break;
			default: 	  languageVars.language = SystemLanguage.English; break;
		}

		/*
		// Identificar idioma
		string language = "en_US";
		switch(lang)
		{
			case SystemLanguage.Portuguese: language = "pt_BR"; break;
			case SystemLanguage.English:    language = "en_US"; break;
			default: 						language = "en_US"; break;
		} */

		// Carregar XML do idioma
		TextAsset xmlAsset = (TextAsset)Resources.Load("Language/" + language, typeof(TextAsset));

		// Criar reader do XML
		XmlReader reader = XmlReader.Create( new StringReader(xmlAsset.text) );

		// Ler o XML
		while(reader.Read())
		{
			// MAIN MENU /////////////////////////////////////////////////////////////////////

			// Main Menu / Start Game
			if (reader.IsStartElement("StartGame"))
			{
				languageVars.main_menu.StartGame.label = reader.GetAttribute("label");
				languageVars.main_menu.StartGame.size = int.Parse(reader.GetAttribute("size"));
			}

			// Main Menu / Config
			if (reader.IsStartElement("Config"))
			{
				languageVars.main_menu.Config.label = reader.GetAttribute("label");
				languageVars.main_menu.Config.size = int.Parse(reader.GetAttribute("size"));
			}

			// Main Menu / About
			if (reader.IsStartElement("About"))
			{
				languageVars.main_menu.About.label = reader.GetAttribute("label");
				languageVars.main_menu.About.size = int.Parse(reader.GetAttribute("size"));
			}

			// Main Menu / Best Score
			if (reader.IsStartElement("BestScore"))
			{
				languageVars.main_menu.BestScore.label = reader.GetAttribute("label");
				languageVars.main_menu.BestScore.size = int.Parse(reader.GetAttribute("size"));
			}

			// CONFIG MENU /////////////////////////////////////////////////////////////////////

			// Config Menu / Configuration
			if (reader.IsStartElement("Configuration"))
			{
				languageVars.config_menu.Configuration.label = reader.GetAttribute("label");
				languageVars.config_menu.Configuration.size = int.Parse(reader.GetAttribute("size"));
			}

			// Config Menu / Quality
			if (reader.IsStartElement("Quality"))
			{
				languageVars.config_menu.Quality.label = reader.GetAttribute("label");
				languageVars.config_menu.Quality.size = int.Parse(reader.GetAttribute("size"));
			}

			// Config Menu / Performance
			if (reader.IsStartElement("Performance"))
			{
				languageVars.config_menu.Performance.label = reader.GetAttribute("label");
				languageVars.config_menu.Performance.size = int.Parse(reader.GetAttribute("size"));
			}

			// Config Menu / BestQuality
			if (reader.IsStartElement("BestQuality"))
			{
				languageVars.config_menu.BestQuality.label = reader.GetAttribute("label");
				languageVars.config_menu.BestQuality.size = int.Parse(reader.GetAttribute("size"));
			}

			// Config Menu / Language
			if (reader.IsStartElement("Language"))
			{
				languageVars.config_menu.Language.label = reader.GetAttribute("label");
				languageVars.config_menu.Language.size = int.Parse(reader.GetAttribute("size"));
			}

			// GAME OVER /////////////////////////////////////////////////////////////////////

			// Game Over / Play Again
			if (reader.IsStartElement("PlayAgain"))
			{
				languageVars.game_over.PlayAgain.label = reader.GetAttribute("label");
				languageVars.game_over.PlayAgain.size = int.Parse(reader.GetAttribute("size"));
			}

			// Game Over / Main Menu
			if (reader.IsStartElement("MainMenu"))
			{
				languageVars.game_over.MainMenu.label = reader.GetAttribute("label");
				languageVars.game_over.MainMenu.size = int.Parse(reader.GetAttribute("size"));
			}

			// OTHERS ////////////////////////////////////////////////////////////////////////

			if (reader.IsStartElement("Loading")) languageVars.others.Loading = reader.ReadElementContentAsString(); // Others / Loading
			if (reader.IsStartElement("Copyright")) languageVars.others.Copyright = reader.ReadElementContentAsString(); // Others / Copyright
			if (reader.IsStartElement("TouchStart")) languageVars.others.TouchStart = reader.ReadElementContentAsString(); // Others / TouchStart
		} 
	}
}
