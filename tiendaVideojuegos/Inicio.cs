﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 
namespace tiendaVideojuegos
{
    public partial class AdminProduct : Form
    {
        public AdminProduct()
        {
            InitializeComponent();
            
            dgvInicio.DataSource = Conexion.llenado("select * from inventario;");
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Busqueda busqueda = new Busqueda();
            busqueda.Show();
            this.Close();
        }

        private void venderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Venta venta = new Venta();
            venta.Show();
            this.Close();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefrescar_Click(object sender, EventArgs e) {
            dgvInicio.DataSource = Conexion.llenado("select * from inventario;");
        }

        private void btnAgregar_Click(object sender, EventArgs e) {
            try
            {
                if (tbTitulo.Text == null)
                {
                    MessageBox.Show("El titulo no puede estar vacío!", "Título vacío", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (tbDescripcion == null)
                    {
                        MessageBox.Show("La descripción no puede estar vacía!", "Descripción vacío", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (cbGenero == null)
                        {
                            MessageBox.Show("El titulo no puede estar vacío!", "Título vacío", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (cbClasificacion == null)
                            {
                                MessageBox.Show("El titulo no puede estar vacío!", "Título vacío", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
                byte[] producto;
                Conexion.conectar();
                if (tbImagen.Text != "")
                {
                    producto = convertirAvatarAByte(tbImagen.Text);
                }
                else
                {
                    producto = convertirAvatarAByte("vacio.jpg");
                }

                cmd.Connection = Conexion.conexion;
                cmd.CommandText = "INSERT INTO `tiendavideojuegos`.`inventario` (`id`, `titulo`, `descripcion`, `precio`, `genero`, `plataforma`, `clasificacion`, `numexistentes`, `ubicacion`,`imagen`) VALUES (NULL, '" + tbTitulo.Text + "', '" + tbDescripcion.Text + "', '" + tbPrecio.Text + "', '" + cbGenero.SelectedItem.ToString() + "', '" + cbPlataforma.SelectedItem.ToString() + "', '" + cbClasificacion.SelectedItem.ToString() + "', '" + tbPiezas.Text + "', '" + cbUbicacion.SelectedItem.ToString() + "',@imagen)";
                cmd.Parameters.Add("@imagen", MySqlDbType.Blob, producto.Length).Value = producto;
                cmd.ExecuteNonQuery();
                // Conexion.comandos(cmd.ToString());
                //Conexion.comandos("INSERT INTO `tiendavideojuegos`.`inventario` (`id`, `titulo`, `descripcion`, `precio`, `genero`, `plataforma`, `clasificacion`, `numexistentes`, `ubicacion`) VALUES (NULL, '" + tbTitulo.Text + "', '" + tbDescripcion.Text + "', '" + tbPrecio.Text + "', '" + cbGenero.SelectedItem.ToString() + "', '" + cbPlataforma.SelectedItem.ToString()+ "', '" + cbClasificacion.SelectedItem.ToString() + "', '" + tbPiezas.Text + "', '" + cbUbicacion.SelectedItem.ToString() + "',"+producto+")");                    
                //Conexion.comandos("UPDATE usuarios SET Avatar = @avatar WHERE NombreUsuario = '" + tb.Text + "'";
                //Query.Parameters.Add("@avatar", MySqlDbType.MediumBlob, avatar.Length).Value = avatar;
                // Query.ExecuteNonQuery();
                dgvInicio.DataSource = Conexion.llenado("select * from inventario;");
                tbTitulo.Text = "";
                tbImagen.Text = "";
                tbPiezas.Text = "";
                tbDescripcion.Text = "";
                tbPrecio.Text = "";
                Image nula = null;
                pbImagen.Image = nula;
                cbClasificacion.Text = "";
                cbGenero.Text = "";
                cbPlataforma.Text = "";
                cbUbicacion.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo no ha salido bien, verifique sus datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        public static byte[] convertirAvatarAByte(string filePath) {
            byte[] avatar;
                FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);

                avatar = reader.ReadBytes((int)stream.Length);

                reader.Close();
                stream.Close();

            return avatar;
        }

        private void btnEliminar_Click(object sender, EventArgs e) {
            Conexion.comandos("DELETE FROM `tiendavideojuegos`.`inventario` WHERE `id`='"+dgvInicio.Rows[dgvInicio.CurrentRow.Index].Cells[dgvInicio.CurrentCell.ColumnIndex].Value.ToString() + "'"+";");
            dgvInicio.DataSource = Conexion.llenado("select * from inventario;");
        }
       
        private void btnActualizar_Click(object sender, EventArgs e) {
           
            
            //Conexion.comandos("UPDATE `tiendavideojuegos`.`inventario` SET `titulo` = '" + datos[1] + "', `descripcion` = '" + datos[2] + "', `precio` = '" + datos[3] + "', `genero` = '" + datos[4] + "', `plataforma` = '" + datos[5] + "', `clasificacion` = '" + datos[6] + "', `numexistentes` = '" + datos[7] + "', `ubicacion` = '" + datos[8] + "', `imagen` = '" + datos[9] + "' WHERE `inventario`.`id` = '" + datos[0] + "';");
            try
            {
                Conexion.conectar();
                byte[] producto = convertirAvatarAByte(tbImagen.Text);
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
                cmd.Connection = Conexion.conexion;
                cmd.CommandText = "UPDATE `tiendavideojuegos`.`inventario` SET `titulo` = '" + tbTitulo.Text + "', `descripcion` = '" + tbDescripcion.Text + "', `precio` = '" + tbPrecio.Text + "', `genero` = '" + cbGenero.Text + "', `plataforma` = '" + cbPlataforma.Text + "', `clasificacion` = '" + cbClasificacion.Text + "', `numexistentes` = '" + tbPiezas.Text + "', `ubicacion` = '" + cbUbicacion.Text + "', `imagen` = @imagen WHERE `inventario`.`id` = '" + dgvInicio.CurrentRow.Cells[0].Value.ToString() + "';";
                cmd.Parameters.Add("@imagen", MySqlDbType.Blob, producto.Length).Value = producto;
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Conexion.comandos("UPDATE `tiendavideojuegos`.`inventario` SET `titulo` = '" + tbTitulo.Text + "', `descripcion` = '" + tbDescripcion.Text + "', `precio` = '" + tbPrecio.Text + "', `genero` = '" + cbGenero.Text + "', `plataforma` = '" + cbPlataforma.Text + "', `clasificacion` = '" + cbClasificacion.Text + "', `numexistentes` = '" + tbPiezas.Text + "', `ubicacion` = '" + cbUbicacion.Text + "' WHERE `inventario`.`id` = '" + dgvInicio.CurrentRow.Cells[0].Value.ToString() + "';");
            }
            
            dgvInicio.DataSource = Conexion.llenado("select * from inventario;");
            btnAgregar.Enabled = true;
            btnActualizar.Enabled = false;
            tbImagen.Text = "";
            tbDescripcion.Text = "";
            tbPiezas.Text = "";
            tbPrecio.Text = "";
            tbTitulo.Text = "";
            cbClasificacion.Text = "";
            cbGenero.Text = "";
            cbPlataforma.Text = "";
            cbUbicacion.Text = "";
        }

        private void btnAgregarCompradas_Click(object sender, EventArgs e) {
            Conexion.comandos("call tiendavideojuegos.agregarPiezas(" + dgvInicio.CurrentRow.Cells[0].Value.ToString() + "," + tbPiezascompradas.Text + ");");
            tbPiezascompradas.Text = "";
            dgvInicio.DataSource = Conexion.llenado("select * from inventario;");
        }

        private void btnImagen_Click(object sender, EventArgs e) {
            OpenFileDialog directorio = new OpenFileDialog();
            directorio.Filter = "Archivos .jpg|*.jpg|  Archivos .bmp|*.bmp";
            if (directorio.ShowDialog() == DialogResult.OK) {
                tbImagen.Text = directorio.FileName;
                pbImagen.Image = Image.FromFile(directorio.FileName);
            }
        }

        private void dgvInicio_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAgregar.Enabled = false;
            btnActualizar.Enabled = true;
            tbDescripcion.Text = dgvInicio.CurrentRow.Cells[2].Value.ToString();
            tbImagen.Text = "";
            tbPiezas.Text = dgvInicio.CurrentRow.Cells[7].Value.ToString();
            tbPrecio.Text = dgvInicio.CurrentRow.Cells[3].Value.ToString();
            tbTitulo.Text = dgvInicio.CurrentRow.Cells[1].Value.ToString();
            cbClasificacion.Text = dgvInicio.CurrentRow.Cells[6].Value.ToString();
            cbGenero.Text = dgvInicio.CurrentRow.Cells[4].Value.ToString();
            cbPlataforma.Text = dgvInicio.CurrentRow.Cells[5].Value.ToString();
            cbUbicacion.Text = dgvInicio.CurrentRow.Cells[8].Value.ToString();
            try
            {

                MemoryStream ms = new MemoryStream((byte[])dgvInicio.CurrentRow.Cells[9].Value);
                pbImagen.Image = Image.FromStream(ms);

            }
            catch (Exception ex)
            {
                Image imagen = null;
                pbImagen.Image = imagen;
            }

        }

        private void tbPiezas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void tbPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void verVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ventascs ventas = new Ventascs();
            ventas.Show();
            this.Close();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuario.cerrarSesion();
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuarios usuarios = new Usuarios();
            usuarios.Show();
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            tbTitulo.Text = "";
            tbDescripcion.Text = "";
            tbImagen.Text = "";
            tbPiezas.Text = "";
            tbPrecio.Text = "";
            cbClasificacion.Text = "";
            cbGenero.Text = "";
            cbPlataforma.Text = "";
            cbUbicacion.Text = "";
            Image imagen = null;
            pbImagen.Image = imagen;
            btnActualizar.Enabled = false;
            btnAgregar.Enabled = true;
        }
    }
}
