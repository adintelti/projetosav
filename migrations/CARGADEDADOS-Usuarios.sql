USE [DESCOMPLICARROS];

INSERT INTO USUARIOS (
IdTipoUsuario
,NomeUsuario
,CpfUsuario
,DtNascimentoUsuario
,EmailUsuario
,Senha
,Ativo
,IdUsuarioCadastro
)
VALUES(1, --IdTipoUsuario
'Admininstrador', --Nome
'12345678910', --Cpf
'01/01/2023', --DtNascimento
'adm@projetosav.com', -- email
'12345', --senha
1, --Ativo
1); -- IdUsuarioCadastro

INSERT INTO USUARIOS (
IdTipoUsuario
,NomeUsuario
,CpfUsuario
,DtNascimentoUsuario
,EmailUsuario
,Senha
,Ativo
,IdUsuarioCadastro
)
VALUES(2, --IdTipoUsuario
'Davi', --Nome
'2345678910', --Cpf
'10/27/1999', --DtNascimento
'davitestes@gmail.com', -- email
'6789', --senha
1, --Ativo
1); -- IdUsuarioCadastro