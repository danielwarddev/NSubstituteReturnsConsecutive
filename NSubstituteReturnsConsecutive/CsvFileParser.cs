namespace NSubstituteReturnsConsecutive;

public class CsvFileParser
{
    private readonly ICsvLineParser _csvLineParser;
    private readonly string[] _specialCommaValues;

    public CsvFileParser(ICsvLineParser csvLineParser, string[] specialCommaValues)
    {
        _csvLineParser = csvLineParser;
        _specialCommaValues = specialCommaValues;
    }
    
    /*
     * Example case: you need to parse a CSV file, but the file you're receiving sometimes contains
     * rows where the values themselves have commas inside of them
     * 
     * There are some holes in the business logic of this method, such as a lack of trimming strings
     * and not checking for duplicate keys in the dictionary before adding them, but don't pay too much
     * attention to the business logic itself. The focus of this repo is really meant to be the NSubstitute extension
     */
    public Dictionary<string, int> ParseLineValues(string line)
    {
        var values = new Dictionary<string, int>();
        
        int i = 0;
        while (i < _specialCommaValues.Length && line != string.Empty)
        {
            var lineData = _csvLineParser.ExtractValueFromLine(line);
            values.Add(_specialCommaValues[i], lineData.Occurrences);
            line = lineData.LineWithoutValue; 
            i++;
        }

        foreach (var value in line.Split(','))
        {
            values.Add(value, 1);
        }

        return values;
    }
}

public interface ICsvLineParser
{
    LineData ExtractValueFromLine(string line);
}

public class CsvLineParser : ICsvLineParser
{
    public LineData ExtractValueFromLine(string line)
    {
        throw new NotImplementedException();
    }
}

public record LineData(int Occurrences, string LineWithoutValue);