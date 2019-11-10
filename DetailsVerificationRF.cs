using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DetailsVerificationRF
{
    public static class DetailsVerificationRF
    {
        public static bool ValidateBIK(string bik, out string error)
        {
       
            if (String.IsNullOrWhiteSpace(bik))
            {
                error = "БИК пуст";
                return false;
            }
            else if (IsDigitalOnly(bik))
            {
                error = "БИК может состоять только из цифр";
                return false;
            }
            else if (bik.Length != 9)
            {
                error = "БИК может состоять только из 9 цифр";
                return false;
            }
            error = "";
            return true;
        }

        public static bool ValidateBIK(string bik)
        {
            string err;
            return ValidateBIK(bik,out err);
        }

        private static bool IsDigitalOnly(string number)
        {
            return !(number.Length == number.Where(c => char.IsDigit(c)).Count());
        }
        public static bool ValidateINN(string inn)
        {
            string err;
            return ValidateINN(inn,out err);
        }
        public static bool ValidateKpp(string kpp)
        {
            string err;
            return ValidateKpp(kpp, out err);
        }

            public static bool ValidateINN(string inn, out string error)
        {

            error = "";
            if (String.IsNullOrWhiteSpace(inn))
            {
                error = "ИНН пуст";
                return false;
            }
            else if (IsDigitalOnly(inn))
            {
                error = "ИНН может состоять только из цифр";
                return false;
            }
            else if (inn.Length != 10 && inn.Length != 12)
            {
                error = "ИНН может состоять только из 10 или 12 цифр";
                return false;
            }
            else
            {
                switch (inn.Length)
                {
                    case 10:
                        {
                            var n10 = checkDigit(inn, new int[] { 2, 4, 10, 3, 5, 9, 4, 6, 8 });
                            if (n10 == int.Parse(inn[9].ToString()))
                            {
                                return true;
                            }
                            else
                            {
                                error = "Неправильное контрольное число";
                                return false;
                            }
                           
                        }
                    case 12:
                        {
                            var n11 = checkDigit(inn, new int[] { 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 });
                            var n12 = checkDigit(inn, new int[] { 3, 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 });
                            if ((n11 == int.Parse(inn[10].ToString())) && (n12 == int.Parse(inn[11].ToString())))
                            {
                                
                                return true;
                            }
                            else
                            {
                                error = "Неправильное контрольное число";
                                return false;
                            }
                            
                        }
                }

                return false;
            }
        }

        private static int checkDigit(string inn, int[] coefficients)
        {
            var n = 0;
            for (int i = 0; i < coefficients.Length; i++)
            {
                {
                    n += coefficients[i] * int.Parse(inn[i].ToString());
                }
            }
            return (n % 11 % 10);
        }

     

            public static bool ValidateKpp(string kpp, out string error)
        {


            error = "";
            if (String.IsNullOrWhiteSpace(kpp))
            {
                error = "КПП пуст";
                return false;
            }
            else if (kpp.Length != 9)
            {
                error = "КПП может состоять только из 9 знаков (цифр или заглавных букв латинского алфавита от A до Z)";
                return false;
            }
            else if (!Regex.IsMatch(kpp, @"^[0-9]{4}[0-9A-Z]{2}[0-9]{3}$"))
            {
                error = "Неправильный формат КПП";
                return false;
            }
            return true;

        }
        public static bool ValidateKs(string ks, string bik)
        {
            string err;
            return ValidateKs(ks, bik, out err);
        }

            public static bool ValidateKs(string ks, string bik,out string error)
        {
            if (ValidateBIK(bik,out error))
            {                           
                if (String.IsNullOrWhiteSpace(ks))
                {   
                    error = "К/С пуст";
                    return false;
                }
                else if (IsDigitalOnly(ks))
                {
                    error = "К/С может состоять только из цифр";
                    return false;
                }
                else if (ks.Length != 20)
                {
                    error = "К/С может состоять только из 20 цифр";
                    return false;
                }
                else
                {
                    var bikKs = '0' + bik.Substring(4, 2) + ks;
                    var checksum = 0;
                    var coefficients = new int[] { 7, 1, 3, 7, 1, 3, 7, 1, 3, 7, 1, 3, 7, 1, 3, 7, 1, 3, 7, 1, 3, 7, 1 };
                    for (int i = 0; i < bikKs.Length; i++)
                    {
                        checksum += coefficients[i] * (int.Parse(bikKs[i].ToString()) % 10);
                    }
                    if (checksum % 10 == 0)
                    {
                        return true;
                    }
                    else
                    {
                        error = "Неправильное контрольное число";
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool ValidateOgrn(string ogrn)
        {
            string err;
            return ValidateOgrn(ogrn, out err);
        }
            public static bool ValidateOgrn(string ogrn,out string error)
        {
            error = "";
            if (String.IsNullOrWhiteSpace(ogrn))
            {            
                error = "ОГРН пуст";
                return false;
            }
            else if (IsDigitalOnly(ogrn))
            {       
                error = "ОГРН может состоять только из цифр";
                return false;
            }
            else if (ogrn.Length != 13)
            {               
                error = "ОГРН может состоять только из 13 цифр";
                return false;
            }
            else
            {
                var n13 = (long.Parse(ogrn.Substring(0, ogrn.Length - 1)) % 11).ToString();
                var buff = n13.Length==1?n13:n13.Substring(0, n13.Length - 1);
                if (int.Parse(buff) == int.Parse(ogrn[12].ToString()))
                {
                    return true;
                }
                else
                {               
                    error = "Неправильное контрольное число";
                }
            }
            return false;
        }
        public static bool ValidateOgrnip(string ogrnip)
        {
            string err;
            return ValidateOgrnip(ogrnip, out err);
        }
            public static bool ValidateOgrnip(string ogrnip,out string error)
        {
            error = "";
         if (String.IsNullOrWhiteSpace(ogrnip))
            {
                error = "ОГРНИП пуст";
                return false;
            }
            else if (IsDigitalOnly(ogrnip))
            {              
                error = "ОГРНИП может состоять только из цифр";
                return false;
            }
            else if (ogrnip.Length != 15)
            {
                error = "ОГРНИП может состоять только из 15 цифр";
                return false;
            }
            else
            {
                var n15 = (long.Parse(ogrnip.Substring(0, ogrnip.Length- 1)) % 13).ToString();
                var buff = n15.Length == 1 ? n15 : n15.Substring(0, n15.Length - 1);
                if (int.Parse(buff) == int.Parse(ogrnip[14].ToString()))
                {
                    return  true;
                }
                else
                {                  
                    error = "Неправильное контрольное число";
                    return false;
                }
            }
  
        }
        public static bool ValidateRS(string rs, string bik)
        {
            string err;
            return ValidateRS(rs, bik, out err);
        }

            public static bool ValidateRS(string rs, string bik,out string error)
        {
            
            error = "";
            if (ValidateBIK(bik,out error))
            {
               
                if (String.IsNullOrWhiteSpace(rs))
                {
                    error = "Р/С пуст";
                    return false;
                }
                else if (IsDigitalOnly(rs))
                {
                    error = "Р/С может состоять только из цифр";
                    return false;
                }
                else if (rs.Length != 20)
                {
                    error = "Р/С может состоять только из 20 цифр";
                    return false;
                }
                else
                {
                    var bikRs = bik.Substring(bik.Length - 3, 3) + rs;
                    var checksum = 0;
                    var coefficients = new int[] { 7, 1, 3, 7, 1, 3, 7, 1, 3, 7, 1, 3, 7, 1, 3, 7, 1, 3, 7, 1, 3, 7, 1 };
                    for (int i = 0; i < bikRs.Length; i++)
                    {
                        
                            checksum += coefficients[i] * (bikRs[i] % 10);
                        
                    }
                    if (checksum % 10 == 0)
                    {
                        return true;
                    }
                    else
                    {
                        error = "Неправильное контрольное число";
                        return false;
                    }
                }
            }
            return false;

        }
        public static bool ValidateSNILS(string snils)
        {
            string err;
            return ValidateSNILS(snils,out err);
        }
            public static bool ValidateSNILS(string snils,out string error)
        {
            
            error = "";
            if (String.IsNullOrWhiteSpace(snils))
            {
                error = "СНИЛС пуст";
                return false;
            }
            else if (IsDigitalOnly(snils))
            {   
                error = "СНИЛС может состоять только из цифр";
                return false;
            }
            else if (snils.Length != 11)
            {
                error = "СНИЛС может состоять только из 11 цифр";
                return false;
            }
            else
            {
                var sum = 0;
                for (var i = 0; i < 9; i++)
                {
                    sum += int.Parse(snils[i].ToString()) * (9 - i);

                }
                var checkDigit = 0;
                if (sum < 100)
                {
                    checkDigit = sum;
                }
                else if (sum > 101)
                {
                    checkDigit = (sum % 101);
                    if (checkDigit == 100)
                    {
                        checkDigit = 0;
                    }
                }
                if (checkDigit == int.Parse(snils.Substring(snils.Length-2, 2)))
                {
                    return true;
                }
                else
                {                 
                    error = "Неправильное контрольное число";
                    return false;
                }
            }
           
        }


    }
}
