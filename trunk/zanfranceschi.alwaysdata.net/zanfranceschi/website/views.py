from django.shortcuts import get_object_or_404, render_to_response
from django.http import HttpResponseRedirect, HttpResponse
import zanfranceschi
from socket import gethostname

def index(request):
	return render_to_response('index.html', {'hostname' : gethostname()})
	
def curriculo(request):
	return render_to_response('curriculo.html')