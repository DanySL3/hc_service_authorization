namespace Domain.Exceptions
{
    public static class MessageException
    {
        private static readonly Dictionary<int, string> codigoErrores = new Dictionary<int, string>()
        {
            //respuesta a los resultados

            {200,"La petición se completó exitosamente."},
            {204,"La petición se completó exitosamente, pero su respuesta no tiene ningún contenido."},

            {400,"Lo sentimos, la petición no pudo completarse debido a que la solicitud no fue válida."},
            {409,"Lo sentimos, la petición no pudo completarse debido a un conflicto con el estado actual del recurso."},

            {501,"¡Disculpa las molestias! En este momento no podemos atenderte."},
            {500,"Lo sentimos, la petición no pudo completarse debido a un conflicto interno en nuestros servidores."},
        

            //la validación de datos 1001 - 1049

            {1001,"## no puede ser nulo o negativo."},
            {1002,"## no puede estar vacío."},
            {1003,"## fuera de los límites permitidos."},
            {1004,"Longitud permitida (##)."},
            {1005,"Ingrese un tipo de ##."},
            {1006,"Tipo de ## no válido."},
            {1007,"Ingrese un nombre de ##."},
            {1008,"Nombre de ## no válido."},
            {1009,"Ingrese un##."},
            {1010,"## no válido."},
            {1011,"No corresponde ingresar un##."},
            {1012,"Escriba caracteres alfanuméricos (a-z y 0-9)."},
            {1013,"Escriba caracteres permitidos (a-z)."},
            {1014,"Escriba caracteres numéricos (0-9)."},

            //validación de registros 1050 - 1099

            {1051,"No se completo su petición por que: ##."},
            {1052,"No se puede extornar una transacción que extorna a otro"},
            {1053,"El código de pago es invalido"},
            {1054,"Usuario o contraseña es incorrecta."},

            //Otras validaciones adicionales 2000 - 

            {2000,"Error general interno: ##."},
            {2001,"No se pudo completar su petición por que: ##."},
            {2002,"Sin especificar."}
        };

        public static string GetErrorByCode(int nCode, string cName = "elemento")
        {
            string cResult = string.Empty;
            if (codigoErrores.TryGetValue(nCode, out cResult))
                return cResult.Replace("##", cName);
            return "Error desconocido en ##".Replace("##", cName);
        }
    }
}
