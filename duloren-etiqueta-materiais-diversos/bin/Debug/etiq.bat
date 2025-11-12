@echo off
mode com1:9600,n,8,1
type etiq.prn >com1
