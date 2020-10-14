using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace myRSA
{
    /**
     Code created by Alexander Fields https://github.com/roku674  || https://www.linkedin.com/in/alexander-fields-aa57a997/
feel free to donate https://sites.google.com/view/perilousgamesltd/donate
Brave is a save browser that doesn't track your data unless you want it to be tracked. You can then be paied (in cryptocurrency, but still paid nonetheless) for your data https://brave.com/rok079
     **/
    public partial class Form1 : Form
    {
        String publicKey, privateKey; //strings to hold the public and private keys'
        UnicodeEncoding encoder = new UnicodeEncoding();

        public Form1()
        {
            RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider();
            InitializeComponent();
            privateKey = myRSA.ToXmlString(true);
            publicKey = myRSA.ToXmlString(false);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPlainText.Text = "";
            txtPlainText.Refresh();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            var myRSA = new RSACryptoServiceProvider();
            //Set up the cryptoServiceProvider with the proper key
            myRSA.FromXmlString(publicKey);

            //Encode the data to encrypt as a byte array
            var dataToEncrypt = encoder.GetBytes(txtPlainText.Text);

            //Encrypt the byte array
            var encryptedByteArray = myRSA.Encrypt(dataToEncrypt, false).ToArray();

            var length = encryptedByteArray.Count();
            var item = 0;
            var sb = new StringBuilder();

            //Change each byte in the encrypted byte array to text

            foreach (var x in encryptedByteArray)
            {
                item++;
                sb.Append(x);
                if (item < length) sb.Append(",");
            }
            txtCyperText.Text = sb.ToString();
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            var myRSA = new RSACryptoServiceProvider();
            //Split the data into an array
            var dataArray = txtCyperText.Text.Split(new char[] { ','});

            //Convert to bytes
            byte[] dataByte = new byte[dataArray.Length];
            for (int i = 0; i < dataArray.Length; i++) dataByte[i] = Convert.ToByte(dataArray[i]);

            //Decrypt the byte array
            myRSA.FromXmlString(privateKey);
            var decryptedBytes = myRSA.Decrypt(dataByte, false);

            //Place into text box
            txtPlainText.Text = encoder.GetString(decryptedBytes);
        }

        private void btnClear2_Click(object sender, EventArgs e)
        {
            txtCyperText.Text = "";
            txtCyperText.Refresh();
        }
    }
}
