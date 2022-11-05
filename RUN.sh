docker-compose build

docker-compose up -d

URL=$(docker inspect --format='http://localhost:{{(index (index .NetworkSettings.Ports "80/tcp") 0).HostPort}}/swagger' brainlaw-api)

URLFRONT=$(docker inspect --format='http://localhost/' brainlaw-front)

start $URL
start $URLFRONT
