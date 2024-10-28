@echo off
REM Atualiza o pip para a versão mais recente
python -m pip install --upgrade pip

REM Instala as dependências
pip install flet
pip install mysql-connector-python

echo.
echo Todas as dependencias foram instaladas.
pause
