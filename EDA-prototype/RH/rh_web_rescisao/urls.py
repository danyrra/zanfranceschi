from django.conf.urls import patterns, include, url

# Uncomment the next two lines to enable the admin:
from django.contrib import admin
admin.autodiscover()

urlpatterns = patterns('rh_web_rescisao.views',
    # Examples:
    # url(r'^$', 'rh_web.views.home', name='home'),
    # url(r'^rh_web/', include('rh_web.foo.urls')),
	
	url(r'^$', 'index'),
	url(r'^notificar-rescisao/$', 'notificar_rescisao'),
	url(r'^rescisoes/listar/mais-recentes-depois-de-id/(?P<id>\d+)/$', 'rescisoes_buscar'),

    # Uncomment the admin/doc line below to enable admin documentation:
    # url(r'^admin/doc/', include('django.contrib.admindocs.urls')),

    # Uncomment the next line to enable the admin:
    url(r'^admin/', include(admin.site.urls)),
)
