USE [DESCOMPLICARROS];

INSERT INTO TIPOUSUARIOS(
NomeTipoUsuario
,DescricaoTipoUsuario
,Ativo
,IdUsuarioCadastro
)
VALUES(
'Admininstrador', --Nome
'Perfil para cadastrar novas informacoes', --Descricao
1, --Ativo
1 -- IdUsuarioCadastro
);


INSERT INTO TIPOUSUARIOS(
NomeTipoUsuario
,DescricaoTipoUsuario
,Ativo
,IdUsuarioCadastro
)
VALUES(
'Gratuito', --NomeTipo
'Usuario de testes para cliente no plano gratuito', --Descricao
1, --Ativo
2 -- IdUsuarioCadastro
);




