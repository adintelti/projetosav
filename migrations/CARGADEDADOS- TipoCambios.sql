USE [DESCOMPLICARROS];

INSERT INTO VEICULOS (
IdMarcaMontadora
,IdModelo
,IdCarroceria
,IdTipoCambio
,AnoInicioVeiculo
,AnoFimVeiculo
,IdUsuarioCadastro
)
VALUES(
1 -- MarcaMontadora
,1 -- Modelo
,1 -- Carroceria
,1 -- TipoCambio
,'2012' -- AnoInicio
,'2014' -- AnoFim
,1) -- IdUsuarioCadastro
,
(2 -- MarcaMontadora
,2 -- Modelo
,3 -- Carroceria
,2 -- TipoCambio
,'2012' -- AnoInicio
,'2014' -- AnoFim
,1) -- IdUsuarioCadastro
,
(3 -- MarcaMontadora
,3 -- Modelo
,3 -- Carroceria
,3 -- TipoCambio
,'2012' -- AnoInicio
,'2014' -- AnoFim
,1) -- IdUsuarioCadastro

SELECT * FROM VEICULOS