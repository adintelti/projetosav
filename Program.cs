using System;
using System.Data.SqlClient;
using System.Net.Http.Headers;

namespace ExemploConsoleSQLServer
{
    class Program
    {
        static readonly string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=DESCOMPLICARROS;Integrated Security=True;TrustServerCertificate=true;";

        static void Main(string[] args)
        {
            ExigirLogin();

            EscreverMensagem("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }

        static void ExigirLogin()
        {
            EscreverMensagem("Olá, seja bem-vindo ao Projeto SAV!");

            EscreverMensagem("Para continuar, informe o seu cpf ou email cadastrado");

            string login = Console.ReadLine();

            login = "12345678910";

            if (string.IsNullOrEmpty(login))
            {
                EscreverMensagem("Login inválido!");
                return;
            }

            EscreverMensagem("Certo, agora informe a respectiva senha");

            string senha = Console.ReadLine();

            senha = "12345";

            if (string.IsNullOrEmpty(senha))
            {
                EscreverMensagem("Senha inválido!");
                return;
            }

            bool resultadoLogin = ExecutarLogin(login, senha);

            if (!resultadoLogin)
            {
                EscreverMensagem("Login ou senha inválidos!");
                return;
            }

            EscreverMensagem(string.Format("Seja bem vindo, {0}", login));

            ExibirMenuOperacoes();
        }

        static void ExibirMenuOperacoes()
        {
            EscreverMensagem("1. Gerenciar usuários", false);
            EscreverMensagem("2. Gerenciar perfis de usuários", false);
            EscreverMensagem("3. Gerenciar marcas montadoras", false);
            EscreverMensagem("4. Gerenciar modelos", false);
            EscreverMensagem("5. Gerenciar tipos de carrocerias", false);
            EscreverMensagem("6. Gerenciar tipos de cambios", false);
            EscreverMensagem("7. Gerenciar veiculos", false);

            EscreverMensagem("Certo, agora informe a operação desejada");

            string operacao = Console.ReadLine();

            switch (operacao)
            {
                case "1":
                    ExibirGestaoUsuarios();
                    break;
                    case "2":
                        ExibirGestaoPerfilUsuarios();
                    break;
                default:
                    EscreverMensagem("Opção inválida!");
                    break;
            }
        }

        private static void ExibirGestaoUsuarios()
        {
            EscreverMensagem("\n1. Listar todos os usuários", false);
            EscreverMensagem("2. Buscar usuário por id", false);
            EscreverMensagem("3. Criar um novo usuário", false);
            EscreverMensagem("4. Atualizar um usuário", false);
            EscreverMensagem("5. Excluir um usuário", false);
            EscreverMensagem("6. Sair", false);

            EscreverMensagem("Certo, agora informe a operação desejada");


            string operacao = Console.ReadLine();

            switch (operacao)
            {
                case "1":
                    ExibirTodosUsuarios();
                    break;
                case "2":
                    BuscarUsuarioPorId();
                    break;
                case "3":
                    CriarNovoUsuario();
                    break;
                case "4":
                    AtualizarUsuarioPorId();
                    break;
                case "5":
                    ExcluirUsuarioPorId();
                    break;
                default:
                    EscreverMensagem("Opção inválida!");
                    break;
            }
        }
    
        
     private static void ExibirGestaoPerfilUsuarios()
        {
            EscreverMensagem("\n1. Listar todos os perfis usuários", false);
            EscreverMensagem("2. Buscar perfil usuário por id", false);
            EscreverMensagem("3. Criar um novo perfil usuário", false);
            EscreverMensagem("4. Atualizar um perfil usuário", false);
            EscreverMensagem("5. Excluir um perfil usuário", false);
            EscreverMensagem("6. Sair", false);

            EscreverMensagem("Certo, agora informe a operação desejada");


            string operacao = Console.ReadLine();

            switch (operacao)
            {
                case "1":
                    ExibirTodosPerfilUsuarios();
                    break;
                case "2":
                    BuscarPerfilUsuarioPorId();
                    break;
                    break;
                case "3":
                    AtualizarPerfilUsuarioPorId();
                    break;
                case "4":
                    ExcluirPerfilUsuarioPorId();
                    break;
                default:
                    EscreverMensagem("Opção inválida!");
                    break;
            }
        }
        private static void CriarNovoUsuario()
        {
            EscreverMensagem("Certo, agora informe o id do perfil de usuario que seja criar");

            string idPerfilUsuario = Console.ReadLine();

            EscreverMensagem("Certo, agora informe um nome para o usuario");

            string nomeUsuario = Console.ReadLine();

            EscreverMensagem("Certo, agora informe um cpf para o usuario");

            string cpfUsuario = Console.ReadLine();

            EscreverMensagem("Muito bem, agora informe uma data de nascimento(padrão americano mm/dd/aaaa)");

            string dtNascimentoUsuario = Console.ReadLine();

            EscreverMensagem("Certo, agora um email para o usuario");

            string emailUsuario = Console.ReadLine();

            string senhaUsuario, confirmacaoSenhaUsuario;

            do
            {
                //Confirmacao senha
                EscreverMensagem("Certo, agora uma senha para o usuario");
                senhaUsuario = Console.ReadLine();
                EscreverMensagem("Agora confirme a senha");
                confirmacaoSenhaUsuario = Console.ReadLine();

                if (senhaUsuario != confirmacaoSenhaUsuario)
                {
                    EscreverMensagem("As senhas não conferem");
                }
            }
            while (senhaUsuario != confirmacaoSenhaUsuario);

            // Crie uma conexão com o SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Abra a conexão
                    connection.Open();

                    // Exemplo de consulta SELECT
                    string query = string.Format(@"INSERT INTO USUARIOS (IdPerfilUsuario, NomeUsuario, CpfUsuario, DtNascimentoUsuario, EmailUsuario, Senha, Ativo, IdUsuarioCadastro)
                                    VALUES({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7})", idPerfilUsuario, nomeUsuario, cpfUsuario, dtNascimentoUsuario, emailUsuario, senhaUsuario, 1, 1);


                    // Crie um comando SQL e associe à conexão
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int linhasAfetadas = command.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            EscreverMensagem("Registro incluído com sucesso!");
                        }
                        else
                        {
                            EscreverMensagem("Nenhum dado inserido.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    EscreverMensagem("Erro: " + ex.Message);
                }
                finally
                {
                    // Certifique-se de fechar a conexão, independentemente do resultado
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }



        private static void AtualizarUsuarioPorId()
        {
            EscreverMensagem("Certo, agora informe o id do usuário desejado");

            string idUsuario = Console.ReadLine();

            EscreverMensagem("Certo, agora informe o novo nome para o usuario");

            string novoNome = Console.ReadLine();

            // Crie uma conexão com o SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Abra a conexão
                    connection.Open();

                    // Exemplo de consulta SELECT
                    string query = @"UPDATE USUARIOS 
                        SET NomeUsuario = '" + novoNome + "' WHERE IdUsuario = " + idUsuario;

                    // Crie um comando SQL e associe à conexão
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int linhasAfetadas = command.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            EscreverMensagem("Registro atualizado com sucesso!");
                        }
                        else
                        {
                            EscreverMensagem("Nenhum dado atualizado.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    EscreverMensagem("Erro: " + ex.Message);
                }
                finally
                {
                    // Certifique-se de fechar a conexão, independentemente do resultado
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }
        private static void AtualizarPerfilUsuarioPorId()
        {
            EscreverMensagem("Certo, agora informe o id do perfil usuário desejado");

            string idPerfilUsuario = Console.ReadLine();

            EscreverMensagem("Certo, agora informe o novo nome para o perfil usuario");

            string novoNomePerfil = Console.ReadLine();

            // Crie uma conexão com o SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Abra a conexão
                    connection.Open();

                    // Exemplo de consulta SELECT
                    string query = @"UPDATE PERFILUSUARIOS 
                        SET NomeUsuario = '" + novoNomePerfil + "' WHERE IdPerfilUsuario = " + idPerfilUsuario;

                    // Crie um comando SQL e associe à conexão
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int linhasAfetadas = command.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            EscreverMensagem("Registro atualizado com sucesso!");
                        }
                        else
                        {
                            EscreverMensagem("Nenhum dado atualizado.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    EscreverMensagem("Erro: " + ex.Message);
                }
                finally
                {
                    // Certifique-se de fechar a conexão, independentemente do resultado
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        private static void ExcluirUsuarioPorId()
        {
            EscreverMensagem("Certo, agora informe o id do usuário desejado");

            string idUsuario = Console.ReadLine();

            // Crie uma conexão com o SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Abra a conexão
                    connection.Open();

                    // Exemplo de consulta SELECT
                    string query = "DELETE FROM USUARIOS WHERE IdUsuario = " + idUsuario;

                    // Crie um comando SQL e associe à conexão
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int linhasAfetadas = command.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            EscreverMensagem("Dados excluídos com sucesso!");
                        }
                        else
                        {
                            EscreverMensagem("Nenhum dado excluído.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    EscreverMensagem("Erro: " + ex.Message);
                }
                finally
                {
                    // Certifique-se de fechar a conexão, independentemente do resultado
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }
        private static void ExcluirPerfilUsuarioPorId()
        {
            EscreverMensagem("Certo, agora informe o id do usuário desejado");

            string idPerfilUsuario = Console.ReadLine();

            // Crie uma conexão com o SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Abra a conexão
                    connection.Open();

                    // Exemplo de consulta SELECT
                    string query = "DELETE FROM PERFILUSUARIOS WHERE IdPerfilUsuario = " + idPerfilUsuario;

                    // Crie um comando SQL e associe à conexão
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int linhasAfetadas = command.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            EscreverMensagem("Dados excluídos com sucesso!");
                        }
                        else
                        {
                            EscreverMensagem("Nenhum dado excluído.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    EscreverMensagem("Erro: " + ex.Message);
                }
                finally
                {
                    // Certifique-se de fechar a conexão, independentemente do resultado
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }




        private static void BuscarUsuarioPorId()
        {
            EscreverMensagem("Certo, agora informe o id do usuário desejado");

            string idUsuario = Console.ReadLine();

            // Crie uma conexão com o SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Abra a conexão
                    connection.Open();

                    //EscreverMensagem("Conexão bem-sucedida!");

                    // Exemplo de consulta SELECT
                    string query = "SELECT * FROM USUARIOS WHERE IdUsuario = " + idUsuario;

                    // Crie um comando SQL e associe à conexão
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute o comando e obtenha um leitor de dados
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Verifique se existem linhas retornadas
                            if (reader.HasRows)
                            {
                                //exibir o cabeçalho
                                EscreverMensagem("IdUsuario | IdPerfilUsuario | NomeUsuario | CpfUsuario | DtNascimentoUsuario | EmailUsuario | Senha | Ativo | IdUsuarioCadastro | DtCadastro | IdUsuarioUltimaAtualizacao | DtUltimaAtualizacao");
                                // Itere pelas linhas e exiba os resultados
                                while (reader.Read())
                                {
                                    // Supondo que há uma coluna chamada "IdUsuario" na tabela
                                    EscreverMensagem(reader["IdUsuario"] + " | " + reader["IdPerfilUsuario"] + " | " + reader["NomeUsuario"] + " | " + reader["CpfUsuario"] + " | " + reader["DtNascimentoUsuario"] + " | " + reader["EmailUsuario"] + " | " + reader["Senha"] + " | " + reader["Ativo"] + " | " + reader["IdUsuarioCadastro"] + " | " + reader["DtCadastro"] + " | " + reader["IdUsuarioUltimaAtualizacao"] + " | " + reader["DtUltimaAtualizacao"]);
                                }
                            }
                            else
                            {
                                EscreverMensagem("Não foram encontradas linhas.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    EscreverMensagem("Erro: " + ex.Message);
                }
                finally
                {
                    // Certifique-se de fechar a conexão, independentemente do resultado
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }
        private static void BuscarPerfilUsuarioPorId()
        {
            EscreverMensagem("Certo, agora informe o id do perfil usuário desejado");

            string idPerfilUsuario = Console.ReadLine();

            // Crie uma conexão com o SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Abra a conexão
                    connection.Open();

                    //EscreverMensagem("Conexão bem-sucedida!");

                    // Exemplo de consulta SELECT
                    string query = "SELECT * FROM PERFILUSUARIOS WHERE IdPerfilUsuario = " + idPerfilUsuario;

                    // Crie um comando SQL e associe à conexão
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute o comando e obtenha um leitor de dados
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Verifique se existem linhas retornadas
                            if (reader.HasRows)
                            {
                                //exibir o cabeçalho
                                EscreverMensagem(" IdPerfilUsuario | Ativo | IdUsuarioCadastro | DtCadastro | IdUsuarioUltimaAtualizacao | DtUltimaAtualizacao");
                                // Itere pelas linhas e exiba os resultados
                                while (reader.Read())
                                {
                                    // Supondo que há uma coluna chamada "IdUsuario" na tabela
                                    EscreverMensagem(reader["IdPerfilUsuario"] + " | " + reader["NomePerfilUsuario"] + " | " + reader["IdUsuarioCadastro"] + " | " + reader["DtCadastro"] + " | " + reader["IdUsuarioUltimaAtualizacao"] + " | " + reader["DtUltimaAtualizacao"]);
                                }
                            }
                            else
                            {
                                EscreverMensagem("Não foram encontradas linhas.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    EscreverMensagem("Erro: " + ex.Message);
                }
                finally
                {
                    // Certifique-se de fechar a conexão, independentemente do resultado
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        static void ExibirTodosUsuarios()
        {
            // Crie uma conexão com o SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Abra a conexão
                    connection.Open();

                    //EscreverMensagem("Conexão bem-sucedida!");

                    // Exemplo de consulta SELECT
                    string query = "SELECT * FROM USUARIOS";


                    // Crie um comando SQL e associe à conexão
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute o comando e obtenha um leitor de dados
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Verifique se existem linhas retornadas
                            if (reader.HasRows)
                            {
                                //exibir o cabeçalho
                                EscreverMensagem("IdUsuario | IdPerfilUsuario | NomeUsuario | CpfUsuario | DtNascimentoUsuario | EmailUsuario | Senha | Ativo | IdUsuarioCadastro | DtCadastro | IdUsuarioUltimaAtualizacao | DtUltimaAtualizacao");
                                // Itere pelas linhas e exiba os resultados
                                while (reader.Read())
                                {
                                    // Supondo que há uma coluna chamada "IdUsuario" na tabela
                                    EscreverMensagem(reader["IdUsuario"] + " | " + reader["IdPerfilUsuario"] + " | " + reader["NomeUsuario"] + " | " + reader["CpfUsuario"] + " | " + reader["DtNascimentoUsuario"] + " | " + reader["EmailUsuario"] + " | " + reader["Senha"] + " | " + reader["Ativo"] + " | " + reader["IdUsuarioCadastro"] + " | " + reader["DtCadastro"] + " | " + reader["IdUsuarioUltimaAtualizacao"] + " | " + reader["DtUltimaAtualizacao"]);
                                }
                            }
                            else
                            {
                                EscreverMensagem("Não foram encontradas linhas.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    EscreverMensagem("Erro: " + ex.Message);
                }
                finally
                {
                    // Certifique-se de fechar a conexão, independentemente do resultado
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }
        static void ExibirTodosPerfilUsuarios()
        {
            // Crie uma conexão com o SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Abra a conexão
                    connection.Open();

                    //EscreverMensagem("Conexão bem-sucedida!");

                    // Exemplo de consulta SELECT
                    string query = "SELECT * FROM PERFILUSUARIOS";

                    // Crie um comando SQL e associe à conexão
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute o comando e obtenha um leitor de dados
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Verifique se existem linhas retornadas
                            if (reader.HasRows)
                            {
                                //exibir o cabeçalho
                                EscreverMensagem(" IdPerfilUsuario |NomePerfilUsuario| IdUsuarioCadastro | DtCadastro | IdUsuarioUltimaAtualizacao | DtUltimaAtualizacao");
                                // Itere pelas linhas e exiba os resultados
                                while (reader.Read())
                                {
                                    // Supondo que há uma coluna chamada "IdUsuario" na tabela
                                    EscreverMensagem(reader["IdPerfilUsuario"] + " | " + reader["NomePerfilUsuario"] + " | " + reader["IdUsuarioCadastro"] + " | " + reader["DtCadastro"] + " | " + reader["IdUsuarioUltimaAtualizacao"] + " | " + reader["DtUltimaAtualizacao"]);
                                }
                            }
                            else
                            {
                                EscreverMensagem("Não foram encontradas linhas.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    EscreverMensagem("Erro: " + ex.Message);
                }
                finally
                {
                    // Certifique-se de fechar a conexão, independentemente do resultado
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        static bool ExecutarLogin(string login, string senha)
        {
            bool resultadoLogin = false;

            // Crie uma conexão com o SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Abra a conexão
                    connection.Open();

                    //EscreverMensagem("Conexão bem-sucedida!");

                    // Exemplo de consulta SELECT
                    string query = string.Format("SELECT 1 FROM USUARIOS WHERE (CpfUsuario = {0} OR EmailUsuario = {0}) AND SENHA = {1}", login, senha);

                    // Crie um comando SQL e associe à conexão
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute o comando e obtenha um leitor de dados
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Verifique se existem linhas retornadas
                            resultadoLogin = reader.HasRows;
                        }
                    }
                }
                catch (Exception ex)
                {
                    EscreverMensagem("Erro: " + ex.Message);
                }
                finally
                {
                    // Certifique-se de fechar a conexão, independentemente do resultado
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
                return resultadoLogin;
            }
        }

        static void EscreverMensagem(string mensagem, bool pulaLinha = true)
        {
            if (pulaLinha)
                Console.WriteLine("\n" + mensagem);
            else
                Console.WriteLine(mensagem);
        }
    }
}
