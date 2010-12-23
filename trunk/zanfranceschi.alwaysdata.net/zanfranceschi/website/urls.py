from django.conf.urls.defaults import *

urlpatterns = patterns(
		
	'zanfranceschi.website.views',
	(r'^$', 'index'),
	(r'^curriculo$', 'curriculo'),
			
	# Uncomment the next line to enable the admin:
    (r'^admin/', include(admin.site.urls)),
			
	#(r'^/item/search/$', 'item_search'),
	#(r'^/item/detail/(?P<id>\d+)$', 'item_detail'),
)
