USE ADINTELTI;


INSERT INTO  (
NomeColaborador

)
VALUES(
'Davi', --Nome
'48395878842', --Cpf
'27/10/1999', --DtNascimento
'davi.devrodrigues@gmail.com', -- email
1, --Ativo
1); -- IdUsuarioCadastro




CREATE TABLE COLABORADORES
(IdColaborador INT IDENTITY PRIMARY KEY,
NomeColaborador VARCHAR(200) NOT NULL,
DtCadastro DATETIME NOT NULL DEFAULT(GETDATE()));



DROP TABLE COLABORADORES



USE ADINTEL;

INSERT INTO COLA (
NomeColaborador
,EstadoEquipamento
)
VALUES(
'MacbookAir', --Nome
'O equipamento se encontra novo e com capa', --descrição
'Apple', --Marca
'Preço', -- Valor
'Novo'); -- Estado)
