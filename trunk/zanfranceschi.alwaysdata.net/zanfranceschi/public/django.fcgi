#!/usr/bin/python
import os, sys

#_PROJECT_DIR = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
_PROJECT_DIR = '/home/zanfranceschi/zanfranceschi/'
sys.path.insert(0, _PROJECT_DIR)
sys.path.insert(0, 'zanfranceschi')

_PROJECT_NAME = _PROJECT_DIR.split('/')[-1]
os.environ['DJANGO_SETTINGS_MODULE'] = "%s.settings" % _PROJECT_NAME

import site
site.addsitedir('/usr/local/lib/python2.6/site-packages/')

from django.core.servers.fastcgi import runfastcgi
runfastcgi(method="threaded", daemonize="false")