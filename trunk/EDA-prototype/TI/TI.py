#!/usr/bin/env python
import pika
import json

connection = pika.BlockingConnection(pika.ConnectionParameters(host='localhost'))
channel = connection.channel()

channel.exchange_declare(exchange='rh_rescisao', type='fanout')

result = channel.queue_declare(exclusive=True)
queue_name = result.method.queue

channel.queue_bind(exchange='rh_rescisao', queue=queue_name)

print u'Aguardando notificação de rescisões...'

def callback(ch, method, properties, body):
	obj = json.loads(body)
	id = obj[0]['pk']
	nome = obj[0]['fields']['colaborador_nome']
	dpto = obj[0]['fields']['colaborador_dpto']
	print u'Rescisão para o colaborador "%s" recebida' % nome
	

channel.basic_consume(callback,
                      queue=queue_name,
                      no_ack=True)

channel.start_consuming()