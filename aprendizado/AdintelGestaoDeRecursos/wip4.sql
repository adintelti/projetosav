CREATE TABLE EQUIPAMENTOS
(IdEquipamento INT IDENTITY PRIMARY KEY,
NomeEquipamento VARCHAR(200) NOT NULL,
Descricao VARCHAR(300) NOT NULL,
MarcaEquipamento VARCHAR(300) NOT NULL,
ValorCompraEquipamento VARCHAR(300) NOT NULL,
EstadoEquipamento VARCHAR(200) NOT NULL,
DtCadastro DATETIME NOT NULL DEFAULT(GETDATE()));

DROP TABLE EQUIPAMENTOS;

SELECT * FROM COLABORADORES;

SELECT * FROM EQUIPAMENTOS;



INSERT INTO EQUIPAMENTOS (
NomeEquipamento
,Descricao
,MarcaEquipamento
,ValorCompraEquipamento
,EstadoEquipamento
)
VALUES(
'MacbookAir', --Nome
'O equipamento se encontra novo e com capa', --descrição
'Apple', --Marca
'Preço', -- Valor
'Novo'); -- Estado)



DROP TABLE COLABORADORES


INSERT INTO (
NomeEquipamento
,Descricao
,MarcaEquipamento
,ValorCompraEquipamento
,EstadoEquipamento
)
VALUES(
'MacbookAir', --Nome
'O equipamento se encontra novo e com capa', --descrição
'Apple', --Marca
'Preço', -- Valor
'Novo'); -- Estado)