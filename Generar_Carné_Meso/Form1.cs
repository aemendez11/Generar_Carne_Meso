using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generar_Carné_Meso
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string carneActual = txtContenido.Text;
            string nuevoCarne = GenerarNuevoCarne(carneActual);
            txtNuevo.Text = nuevoCarne;
            GenerarCodigoQR(nuevoCarne);
        }

        private string GenerarNuevoCarne(string carneActual)
        {
            int[] vectorControl = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

            if (carneActual.Length != 9)
            {
                MessageBox.Show("El carné debe tener exactamente 9 dígitos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "";
            }

            int[] digitos = new int[9];
            for (int i = 0; i < 9; i++)
            {
                if (!int.TryParse(carneActual[i].ToString(), out digitos[i]))
                {
                    MessageBox.Show("El carné debe contener solo dígitos numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return "";
                }
            }

            int suma = 0;
            for (int i = 0; i < 9; i++)
            {
                suma += digitos[i] * vectorControl[i];
            }

            int digitoVerificacion = (11 - (suma % 11)) % 11;

            return carneActual + digitoVerificacion;
        }

        private void GenerarCodigoQR(string texto)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            pbFoto.Image = qrCode.GetGraphic(5); // Tamaño del código QR
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtContenido.Text = "";
            txtNuevo.Text = "";
            pbFoto.Image = null;
        }
    }
}
