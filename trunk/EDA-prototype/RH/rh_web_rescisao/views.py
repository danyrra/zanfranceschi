from django.http import HttpResponse
from django.shortcuts import render, redirect
from datetime import *
from models import *
from forms import *
from django.core import serializers
import time

def index(request):
	return render(request, 'index.html', { 'form' : RescisaoForm() })
	
def notificar_rescisao(request):
	if request.method != 'POST':
		redirect("/index/")

	form = RescisaoForm(request.POST)
	if form.is_valid():
		form.save()
		return render(request, 'index.html', {'msg' : 'Rescisão notificada.' })
	else:
		return render(request, 'index.html', {'form' : form })
		
def rescisoes_buscar(request, id):
	list = Rescisao.objects.filter(id__gt=id)
	message = serializers.serialize("json", list)
	return HttpResponse(message, mimetype="application/json")