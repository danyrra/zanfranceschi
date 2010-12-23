# Django settings for zanfranceschi project.

from socket import gethostname

HOST = gethostname()

# HOST dependant settings
if HOST == 'http4' or True:
	#alwaysdata
	DATABASES = {
		'default': {
			'ENGINE': 'django.db.backends.sqlite3', 
			'NAME': '/home/zanfranceschi/zanfranceschi/database.db3',
			'USER': '',                      
			'PASSWORD': '',                  
			'HOST': '',                      
			'PORT': '',                      
		}
	}
	MEDIA_ROOT = '/home/zanfranceschi/zanfranceschi/public/'
	MEDIA_URL = 'static'
	TEMPLATE_DIRS = ( '/home/zanfranceschi/zanfranceschi/templates', )

elif HOST == 'zanfranceschi':
	#localhost
	DATABASES = {
		'default': {
			'ENGINE': 'django.db.backends.sqlite3', 
			'NAME': 'D:/Projects/Google SVN/trunk/zanfranceschi.alwaysdata.net/zanfranceschi/database.db3',
			'USER': '',                      
			'PASSWORD': '',                  
			'HOST': '',                      
			'PORT': '',                      
		}
	}
	MEDIA_ROOT = 'D:/Projects/Google SVN/trunk/zanfranceschi.alwaysdata.net/zanfranceschi/public/'
	MEDIA_URL = 'static'
	TEMPLATE_DIRS = ( 'D:/Projects/Google SVN/trunk/zanfranceschi.alwaysdata.net/zanfranceschi/templates', )
	



DEBUG = True
TEMPLATE_DEBUG = DEBUG

ADMINS = (
    # ('Your Name', 'your_email@domain.com'),
)

MANAGERS = ADMINS

TIME_ZONE = 'America/Sao_Paulo'

LANGUAGE_CODE = 'en-us'

FILE_CHARSET = 'ISO-8859-1'

SITE_ID = 1

USE_I18N = True

USE_L10N = True

ADMIN_MEDIA_PREFIX = '/media/'

SECRET_KEY = 'knx(+uubb!b$wwwi*_-wg&x(%41x2=bg8qd+s@r5=*pn(n5oii'

TEMPLATE_LOADERS = (
    'django.template.loaders.filesystem.Loader',
    'django.template.loaders.app_directories.Loader',
#     'django.template.loaders.eggs.Loader',
)

MIDDLEWARE_CLASSES = (
    'django.middleware.common.CommonMiddleware',
    'django.contrib.sessions.middleware.SessionMiddleware',
    'django.middleware.csrf.CsrfViewMiddleware',
    'django.contrib.auth.middleware.AuthenticationMiddleware',
    'django.contrib.messages.middleware.MessageMiddleware',
)

ROOT_URLCONF = 'zanfranceschi.urls'

INSTALLED_APPS = (
    'django.contrib.auth',
    'django.contrib.contenttypes',
    'django.contrib.sessions',
    'django.contrib.sites',
    'django.contrib.messages',
    # Uncomment the next line to enable the admin:
    'django.contrib.admin',
    # Uncomment the next line to enable admin documentation:
    #'django.contrib.admindocs',
	'zanfranceschi.website'
)
