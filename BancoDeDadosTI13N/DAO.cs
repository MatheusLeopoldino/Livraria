using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;//Imports para conexão com o banco de dados
using MySql.Data.MySqlClient;//Imports para realizar comandos no banco

namespace BancoDeDadosTI13N
{
    class DAO
    {
        MySqlConnection conexao;
        public string dados;
        public string resultado;
        //Declarando os vetores:
        public int[] cod;
        public string[] nome;
        public string[] telefone;
        public string[] endereco;
        public int i;
        public string msg;
        public int contador = 0;
        //Contrutor
        public DAO(string nomeDoBancoDeDados)
        {
            conexao = new MySqlConnection("server=localhost;DataBase=" + nomeDoBancoDeDados + ";Uid=root;Password=;");
            try
            {
                conexao.Open();//Solicitando a entrada ao banco de dados
                Console.WriteLine("Entrei!!!");
            }
            catch(Exception e)
            {
                Console.WriteLine("Algo deu errado!\n\n" + e);
                conexao.Close();//Fechando a conexão com banco de dados
            }//fim da tentativa de conexão com o banco de dados
        }//fim do construtor

        //Criar o método INSERIR
       

        public void PreencherVetor()
        {
            string query = "select * from pessoa";//Coletando o dado do BD

            //Instanciando os vetores
            cod      = new int[100];
            nome     = new string[100];
            telefone = new string[100];
            endereco = new string[100];

            //Dar valores iniciais para ele
            for(i = 0; i < 100; i++)
            {
                cod[i] = 0;
                nome[i] = "";
                telefone[i] = "";
                endereco[i] = "";
            }//fim da repetição

            //Criar o comando para coleta de dados
            MySqlCommand coletar = new MySqlCommand(query, conexao);
            //Usar o comando lendo os dados do banco
            MySqlDataReader leitura = coletar.ExecuteReader();

            i = 0;
            while (leitura.Read())
            {
                cod[i]      = Convert.ToInt32(leitura["codigo"]);
                nome[i]     = leitura["nome"] + "";
                telefone[i] = leitura["telefone"] + "";
                endereco[i] = leitura["endereco"] + "";
                i++;
                contador++;
            }//fim do while

            //Fechar o dataReader
            leitura.Close();
        }//fim do preencher Vetor

        public string ConsultarTudo()
        {
            //Preencher o vetor
            PreencherVetor();
            msg = "";
            for(int i=0; i < contador; i++)
            {
                msg += "\n\nCódigo: " + cod[i]
                    + ", Nome: " + nome[i]
                    + ", Telefone: " + telefone[i]
                    + ", Endereço: " + endereco[i];       
            }//fim do for
            return msg; 
        }//fim do consultarTudo

        public string ConsultarcodigodoCartao(int codigo)
        {
            PreencherVetor();
            for(int i=0; i < contador; i++)
            {
                if(codigo == cod[i])
                {
                    return nome[i];
                }
            }//fim do for
            return "Código não encontrado!";
        }//fim do consultarNome

        public string ConsultardataCompra(int codigo)
        {
            PreencherVetor();
            for (int i = 0; i < contador; i++)
            {
                if (codigo == cod[i])
                {
                    return telefone[i];
                }
            }//fim do for
            return "Código não encontrado!";
        }//fim do consultarTelefone

        public string ConsultarvalorTotal(int codigo)
        {
            PreencherVetor();
            for (int i = 0; i < contador; i++)
            {
                if (codigo == cod[i])
                {
                    return endereco[i];
                }
            }//fim do for
            return "Código não encontrado!";
        }//fim do consultarEndereço

        public void Atualizar(string campo, string novoDado, int codigo)
        {
            try
            {
                resultado = "update pessoa set " + campo + " = '" +
                            novoDado + "' where codigo = '" + codigo + "'";
                //Executar o script
                MySqlCommand sql = new MySqlCommand(resultado, conexao);
                resultado = "" + sql.ExecuteNonQuery();
                Console.WriteLine("Dado Atualizado com Sucesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Algo deu errado!" + e);
            }
        }//fim do atualizar

        public void Deletar(int codigo)
        {
            resultado = "delete from pessoa where codigo = '" + codigo + "'";
            //Executar o comando
            MySqlCommand sql = new MySqlCommand(resultado, conexao);
            resultado = "" + sql.ExecuteNonQuery();
            //Mensagem...
            Console.WriteLine("Dados Excluídos com sucesso!");
        }//fim do deletar


    }//fim da classe
}//fim do projeto
