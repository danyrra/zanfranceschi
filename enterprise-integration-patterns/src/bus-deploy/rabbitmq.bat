copy /Y .erlang.cookie %HOMEPATH%
copy /Y .erlang.cookie %WINDIR%
net stop rabbitmq
net start rabbitmq
rabbitmqctl stop_app && rabbitmqctl reset && rabbitmqctl cluster rabbit@INFO1301040072 && rabbitmqctl start_app