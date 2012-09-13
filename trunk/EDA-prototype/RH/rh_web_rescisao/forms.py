from django import forms
from django.forms import ModelForm
from models import *

class RescisaoForm(ModelForm):
	class Meta:
		model = Rescisao
		