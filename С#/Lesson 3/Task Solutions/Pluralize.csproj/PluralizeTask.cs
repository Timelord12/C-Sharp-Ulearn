namespace Pluralize
{
	public static class PluralizeTask
	{
		public static string PluralizeRubles(int count)
		{
			/*
			 *  Странный свитч.
			 *  У switch есть кроме case еще ветка default (все, что не попало в кейсы)
			 *  Это лучше Return в конце, своим видом он пугает, будто может произойти, хехе 
			 * 
			 *  Да и проще и красивее было на if'aх, лол.
			 *  -- Jarl
			 */
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