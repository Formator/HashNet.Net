FROM ubuntu:18.04
LABEL version="1.0"
LABEL name="tolar_daemon_sichain"
LABEL Author="Ervin Marguc s.p."
LABEL maintainer="ervin@enteralpha.com"

# RUN apt-get update \
#   && apt-get -y upgrade \
#   && apt-get -y install \
#   unzip \
#   && apt-get clean \
#   && rm -rf /var/lib/apt/lists/*

WORKDIR /daemon

#RUN mkdir -p /daemon/.tolar/keystore/Thin_node/
#RUN mkdir -p /daemon/.tolar/logs/

COPY ./linux/tolar_daemon_linux .
#RUN unzip tolar_daemon_linux.zip
#RUN rm tolar_daemon_linux.zip
RUN chmod a+x tolar_daemon_linux

COPY ./linux/start.sh .
RUN chmod a+x start.sh

COPY ./config/config.json .

EXPOSE 9200 9600

#ENTRYPOINT ["/daemon/tolar_daemon_linux" , "--log_dir=/.tolar/logs", "--config_path=config.json" ]
ENTRYPOINT ["./start.sh" ]