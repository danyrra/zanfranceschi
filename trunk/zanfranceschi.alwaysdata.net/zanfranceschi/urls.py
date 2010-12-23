from django.conf.urls.defaults import *
from zanfranceschi import settings

urlpatterns = patterns('',

    (r'^', include('zanfranceschi.website.urls')),
	
	(r'^static/(?P<path>.*)$', 'django.views.static.serve',  
	{'document_root' : settings.MEDIA_ROOT}),
)

