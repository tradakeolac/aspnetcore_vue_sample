cd src
for /d %%d in ("Saleman.*") do cd %%d & call window_build.cmd & cd .. 
cd ..