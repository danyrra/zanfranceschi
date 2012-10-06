import pika
import sys
from django.db import models
from django.core import serializers

class Colaborador(models.Model):
	nome = models.CharField(max_length=50)
	empresa = models.CharField(max_length=50)
	dpto = models.CharField(max_length=50)
	centro_custo = models.CharField(max_length=50)
	
	def __unicode__(self):
		return self.nome

class Rescisao(models.Model):
	data_rescisao = models.DateField()
	#colaborador = models.ForeignKey(Colaborador)
	
	colaborador_nome = models.CharField(max_length=50)
	colaborador_empresa = models.CharField(max_length=50)
	colaborador_dpto = models.CharField(max_length=50)
	colaborador_centro_custo = models.CharField(max_length=50)
	
	def save(self, *args, **kwargs):
		# persists the object...
		super(Rescisao, self).save(*args, **kwargs)
		
		# serializes the object
		message = serializers.serialize("json", [self,])
		
		#sends the message
		connection = pika.BlockingConnection(pika.ConnectionParameters(host='localhost'))
		channel = connection.channel()
		channel.exchange_declare(exchange='rh', type='fanout')
		channel.basic_publish(exchange='rh', routing_key='', body=message)
		connection.close()
		
		print >> sys.stderr, message
		
		