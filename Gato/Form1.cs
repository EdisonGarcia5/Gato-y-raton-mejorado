using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gato
{
    public partial class Form1 : Form
    {
        private Button[] buttons;

        private int puntosGato = 0; 
        private int puntosRaton = 0;

        private bool Turno = true; 
        private int movimientos = 0; 
        private bool jugando = false;

        private bool usuarioJuegaConX; 
        private bool maquinaJuegaConO;

        public Form1()
        {
            InitializeComponent(); buttons = new Button[] 
            { 
                button1, button2, button3, 
                button4, button5, button6, 
                button7, button8, button9 
            }; 
            
            foreach (Button button in buttons)
            {
                button.Enabled = false; // Desactivar los botones del tablero
                button.Click += BotonClick; 
            } 
        }


        // Sector 1

        // En esta parte se pondra la logica para los botones que utilizamos como tablero.
        private void BotonClick(object sender, EventArgs e) 
        { 
            Button boton = (Button)sender; // Button busca en el arreglo todos los botones
                                       // boton mustra adentro de los botones el texto puesto en esta bariable.

            if (jugando) 
            { 
                if (Turno) 
                { 
                    boton.Text = usuarioJuegaConX ? "X" : "O";
                } 
                else 
                { 
                    boton.Text = usuarioJuegaConX ? "O" : "X";
                }
                Turno = false; // Permite que responda la maquina al estar Desactivado el turno del Jugador.
                movimientos++;

                if (Combinaciones()) 
                {
                    // Verifica quien gana la partida dependiendo de la puntiacion.
                    string ganador;
                    if (!Turno)
                    {
                        ganador = usuarioJuegaConX ? "Gato" : "Ratón";
                        puntosGato += usuarioJuegaConX ? 1 : 0;
                        puntosRaton += usuarioJuegaConX ? 0 : 1;
                    }
                    else
                    {
                        ganador = usuarioJuegaConX ? "Ratón" : "Gato";
                        puntosGato += usuarioJuegaConX ? 0 : 1;
                        puntosRaton += usuarioJuegaConX ? 1 : 0;
                    }
                    
                    // Muestra el nombre del ganador de la partida.
                    MessageBox.Show( "El " + ganador + " es el ganador!");
                    
                    // Muestra en el Label1 el punto al ganador.
                    label1.Text = "🐱: " + puntosGato + "    🐭: " + puntosRaton; 
                    
                    jugando = false; // termina el primer juego.
                    
                }
                else if (movimientos == 9)
                {
                    MessageBox.Show("¡Empate!"); 
                    jugando = false;
                }
                if (jugando && !Turno)
                {
                    JugarMaquina();
                }
            }
        }


        // Sector 2 

        // Juega la maquina, bloque las jugadas del usuario y notifica si gana la maquina usando a la "O" o "X".
        private void JugarMaquina() 
        { 
            Random random = new Random(); 
            int indice = random.Next(buttons.Length); 
            Button boton = buttons[indice];

            while (boton.Text != "") 
            { 
                indice = random.Next(buttons.Length); 
                boton = buttons[indice]; 
            }

            // Verificar si el usuario está a punto de ganar en su próximo movimiento
            for (int i = 0; i < buttons.Length; i++) 
            { 
                if (buttons[i].Text == "") 
                { 
                    buttons[i].Text = usuarioJuegaConX ? "X" : "O"; // Simular movimiento del usuario
                    if (Combinaciones()) 
                    { 
                      boton = buttons[i]; // Bloquear la jugada del usuario
                      break; 
                    } 
                    buttons[i].Text = ""; // Deshacer simulación del movimiento del usuario
                } 
            }

            // Hacer el movimiento de la máquina
            boton.Text = usuarioJuegaConX ? "O" : "X";

            Turno = true; // Al estar activado permite responder al movimiento del usuario.
            
            movimientos++;


            // Verificar si la máquina ganó o hubo empate
            if (Combinaciones())
            {
                string ganador = usuarioJuegaConX ? "Ratón" : "Gato";
                puntosGato += usuarioJuegaConX ? 0 : 1;
                puntosRaton += usuarioJuegaConX ? 1 : 0;

                MessageBox.Show("El " + ganador + " es el ganador!");
                label1.Text = "🐱: " + puntosGato + "    🐭: " + puntosRaton; 
                jugando = false;
                
            }
            else if (movimientos == 9)
            {
                MessageBox.Show("¡Empate!"); 
                jugando = false;
            }
        }


        // Sector 3 

        // Verificar todas las combinaciones ganadoras
        private bool Combinaciones() 
        { 
            if ( 
               (buttons[0].Text == buttons[1].Text && buttons[1].Text == buttons[2].Text && buttons[0].Text != "") || 
               (buttons[3].Text == buttons[4].Text && buttons[4].Text == buttons[5].Text && buttons[3].Text != "") || 
               (buttons[6].Text == buttons[7].Text && buttons[7].Text == buttons[8].Text && buttons[6].Text != "") || 

               (buttons[0].Text == buttons[3].Text && buttons[3].Text == buttons[6].Text && buttons[0].Text != "") || 
               (buttons[1].Text == buttons[4].Text && buttons[4].Text == buttons[7].Text && buttons[1].Text != "") || 
               (buttons[2].Text == buttons[5].Text && buttons[5].Text == buttons[8].Text && buttons[2].Text != "") ||
               
               (buttons[0].Text == buttons[4].Text && buttons[4].Text == buttons[8].Text && buttons[0].Text != "") || 
               (buttons[2].Text == buttons[4].Text && buttons[4].Text == buttons[6].Text && buttons[2].Text != "") 
               ) 
            { 
                return true; 
            } 
            return false;
        }


        // Sector 4 botones

        // inicia el juego para activar los botones de la tabla. 
        private void IniciarJuego_Click(object sender, EventArgs e)
        {
            // seleccionar con cual quiero jugar "X" o "O" 

            if (RBX.Checked) // RadioButton "X"
            {
                labelUsuario.Text = "Usuario";
                labelMaquina.Text = "Máquina";

                usuarioJuegaConX = true; // Activa la opcion X
                maquinaJuegaConO = false; // Desactivado la opcion O
            }
            else if (RBO.Checked) // RadioButton "O"
            {
                labelUsuario.Text = "Máquina";
                labelMaquina.Text = "Usuario";

                usuarioJuegaConX = false; // Desactivado la opcion X
                maquinaJuegaConO = true;  // Activa la opcion O
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una opción");
                return;
            }

            gbOpciones.Enabled = false; // Desactivado opcones de los radionbutton
            IniciarJuego.Enabled = false; // Desactivado boton de inicair juego

            foreach (Button button in buttons)
            {
                button.Enabled = true; // Activar los botones del tablero
            }

            jugando = true;

        }


        // borra los espacios de los botones pero mantiene los puntos 
        private void Nuevojuego_Click(object sender, EventArgs e) 
        { 
            foreach (Button button in buttons) 
            { 
                button.Text = ""; 
            }

            Turno = true;
            movimientos = 0;
            jugando = true;

            gbOpciones.Enabled = false; // Desactivado opcones de los radionbutton
            IniciarJuego.Enabled = false; // Desactivado boton de inicair juego

            foreach (Button button in buttons)
            {
                button.Enabled = true; // Activar los botones del tablero
            }
            jugando = true;
        }

        private void Reiniciar_Click(object sender, EventArgs e)
        {
            foreach (Button button in buttons)
            {
                button.Enabled = false; // Desactivar los botones del tablero
                button.Text = "";
            }

            gbOpciones.Enabled = true; // Activa opcones de los radionbutton
            IniciarJuego.Enabled = true; // Activado boton de inicair juego

            // regresa a los signos de pregunta para ver que va hacer la IA y Usuario.
            labelUsuario.Text = "?";
            labelMaquina.Text = "?";

            // Reinicia los puntos.
            puntosGato = 0;
            puntosRaton = 0;
            label1.Text = "🐱: " + puntosGato + "    🐭: " + puntosRaton;

            RBX.Checked = false;
            RBO.Checked = false;

            jugando = false; // Deja de jugar
            Turno = true; // Reinicia el turno
            movimientos = 0; // Reinicia el contador de movimientos
        }

        private void Cerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

