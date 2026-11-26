using System;
using System.IO;
using System.Text;

namespace Presentacion
{
    internal static class PdfTicket
    {
        public static void Generar(string ruta, string contenido)
        {
            string[] lineas = contenido.Replace("\r", string.Empty).Split('\n');
            StringBuilder cuerpo = new StringBuilder();
            cuerpo.AppendLine("BT");
            cuerpo.AppendLine("/F1 10 Tf");
            cuerpo.AppendLine("50 790 Td");
            cuerpo.AppendLine("14 TL");
            foreach (string linea in lineas)
            {
                cuerpo.Append("(").Append(Escapar(linea)).AppendLine(") Tj");
                cuerpo.AppendLine("T*");
            }
            cuerpo.AppendLine("ET");

            byte[] stream = Encoding.ASCII.GetBytes(cuerpo.ToString());
            using (FileStream archivo = File.Create(ruta))
            using (BinaryWriter salida = new BinaryWriter(archivo, Encoding.ASCII))
            {
                Escribir(salida, "%PDF-1.4\n");
                long[] offsets = new long[6];
                offsets[1] = archivo.Position; Escribir(salida, "1 0 obj\n<< /Type /Catalog /Pages 2 0 R >>\nendobj\n");
                offsets[2] = archivo.Position; Escribir(salida, "2 0 obj\n<< /Type /Pages /Kids [3 0 R] /Count 1 >>\nendobj\n");
                offsets[3] = archivo.Position; Escribir(salida, "3 0 obj\n<< /Type /Page /Parent 2 0 R /MediaBox [0 0 612 842] /Resources << /Font << /F1 4 0 R >> >> /Contents 5 0 R >>\nendobj\n");
                offsets[4] = archivo.Position; Escribir(salida, "4 0 obj\n<< /Type /Font /Subtype /Type1 /BaseFont /Courier >>\nendobj\n");
                offsets[5] = archivo.Position; Escribir(salida, "5 0 obj\n<< /Length " + stream.Length + " >>\nstream\n"); salida.Write(stream); Escribir(salida, "endstream\nendobj\n");
                long xref = archivo.Position;
                Escribir(salida, "xref\n0 6\n0000000000 65535 f \n");
                for (int i = 1; i <= 5; i++) Escribir(salida, offsets[i].ToString("D10") + " 00000 n \n");
                Escribir(salida, "trailer\n<< /Size 6 /Root 1 0 R >>\nstartxref\n" + xref + "\n%%EOF");
            }
        }

        private static string Escapar(string valor)
        {
            StringBuilder resultado = new StringBuilder();
            foreach (char caracter in valor)
            {
                char convertido = caracter <= 255 ? caracter : '?';
                if (convertido == '(' || convertido == ')' || convertido == '\\') resultado.Append('\\');
                resultado.Append(convertido);
            }
            return resultado.ToString();
        }

        private static void Escribir(BinaryWriter salida, string texto) { salida.Write(Encoding.ASCII.GetBytes(texto)); }
    }
}
