namespace Pluralize
{
	public static class PluralizeTask
	{
		public static string PluralizeRubles(int count)
		{
			if ((count % 100 > 10) && (count % 100 < 20))
				return "рублей";
			else switch (count % 10)
				{
					case 1:
						return "рубль";
					case 2:
					case 3:
					case 4:
						return "рубля";
					case 5:
					case 6:
					case 7:
					case 8:
					case 9:
					case 0:
						return "рублей";
				}
			return "";
		}
	}
}