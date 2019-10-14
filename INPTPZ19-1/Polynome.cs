using System.Collections.Generic;


namespace INPTPZ1
{
    class Polynome
    {
        public List<ComplexNumber> Coeficients { get; set; }

        public Polynome()
        {
            Coeficients = new List<ComplexNumber>();
        }

        public Polynome Derive()
        {
            Polynome polynome = new Polynome();
            for (int i = 1; i < Coeficients.Count; i++)
            {
                polynome.Coeficients.Add(Coeficients[i].Multiply(new ComplexNumber() { Re = i }));
            }

            return polynome;
        }

        public ComplexNumber Eval(ComplexNumber complexNumber)
        {
            ComplexNumber zeroComplexNumber = ComplexNumber.Zero;
            for (int i = 0; i < Coeficients.Count; i++)
            {
                ComplexNumber coeficientComplexNumber = Coeficients[i];
                ComplexNumber copyComplexNumber = complexNumber;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                    {
                        copyComplexNumber = copyComplexNumber.Multiply(complexNumber);
                    }                        

                    coeficientComplexNumber = coeficientComplexNumber.Multiply(copyComplexNumber);
                }

                zeroComplexNumber = zeroComplexNumber.Add(coeficientComplexNumber);
            }

            return zeroComplexNumber;
        }

        public override string ToString()
        {
            string resultString = string.Empty;
            for (int i = 0; i < Coeficients.Count; i++)
            {
                resultString += Coeficients[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        resultString += 'x';
                    }
                }
                resultString += " + ";
            }
            return resultString;
        }
    }
}
