import matplotlib.pyplot as plt

plt.xlabel('Number of threads')
plt.ylabel('Time [ms]')
plt.title('Performace [|V| = 1e5, |E| = 1e5]')
plt.plot([1, 2, 4, 8, 16], [310, 310, 310, 310, 310], color='red')
plt.plot([1, 2, 4, 8, 16], [450, 365, 300, 290, 285], color='blue')
plt.plot([1, 2, 4, 8, 16], [478, 380, 270, 263, 240], color='green')
plt.plot([1, 2, 4, 8, 16], [494, 370, 295, 278, 290], color='orange')
plt.legend(['Single thread', 'Threads', 'Thread Pool', 'Lock data structures + Thread Pool'])
plt.show()