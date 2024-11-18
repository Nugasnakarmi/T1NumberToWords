namespace T1NumberToWords.Interfaces;

public interface IInterpreterService { 
    public string InterpretNumberToWords(decimal inputNumber, int mode);
}
