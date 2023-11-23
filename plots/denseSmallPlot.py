import matplotlib.pyplot as plt

plt.xlabel('Number of threads')
plt.ylabel('Time [ms]')
plt.title('Performace [|V| = 1e3, |E| = 4,5e5]')
plt.plot([1, 2, 4, 8, 16], [370, 370, 370, 370, 370], color='red')
plt.plot([1, 2, 4, 8, 16], [430, 260, 160, 120, 160], color='blue')
plt.plot([1, 2, 4, 8, 16], [430, 250, 150, 120, 150], color='green')
plt.plot([1, 2, 4, 8, 16], [420, 300, 220, 150, 190], color='orange')
plt.legend(['Single thread', 'Threads', 'Thread Pool', 'Lock data structures + Thread Pool'])
plt.show()