from django.conf.urls.defaults import *

urlpatterns = patterns(
		
		'zanfranceschi.website.views',
			(r'^$', 'index'),
			(r'^curriculo$', 'curriculo'),
			
			#(r'^/item/search/$', 'item_search'),
			#(r'^/item/detail/(?P<id>\d+)$', 'item_detail'),
)
